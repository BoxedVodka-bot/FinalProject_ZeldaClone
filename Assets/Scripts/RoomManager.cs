﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//PURPOSE: Used to spawn enemies when the player enters the room, and kill them off when they leave
//USAGE: Attached to a room object, which is unique to this room
//This script will need to be called by the camera control script whenever it enters a room
public class RoomManager : MonoBehaviour
{
    public bool roomEnter;//Becomes true when the player enters this room, spawning enemies
    public bool roomReset = true;//As long as this is true, you use the Start Enemy List rather than the current enemy list to spawn enemies
    public bool roomLeave;//Becomes true when the player leaves the room, checking to see what enemies have been removed
    public List<Transform> StartEnemyList = new List<Transform>();//A list of all enemies to appear in this room
    public List<bool> StartEnemySpawner = new List<bool>();//Whether this enemy uses a spawner to spawn or not
    public List<Transform> CurrentEnemyList = new List<Transform>();//All enemies that are currently alive in the room
    public List<bool> CurrentEnemySpawner = new List<bool>();
    public Transform EnemySpawnerPrefab;
    public List<Transform> CurrentPickupList = new List<Transform>();
    public Camera myCamera;
    public Transform myPlayer;
    public Tilemap myTilemap;
    public float spawnTimeIncrease;//How time there is to spawn between each enemy

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(roomEnter) {
            //if the room has reset, you create enemies from the starting list
            if(roomReset) {
                while(CurrentEnemyList.Count > 0) {
                    CurrentEnemyList.RemoveAt(0);
                }
                float spawn = 0.01f;
                for(int i = 0; i < StartEnemyList.Count; i++) {
                    if(StartEnemySpawner[i]) {
                        Transform newEnemy = Instantiate(EnemySpawnerPrefab, myCamera.transform.position, Quaternion.Euler(0f, 0f, 0f));
                        CurrentEnemyList.Add(newEnemy);
                        CurrentEnemySpawner.Add(StartEnemySpawner[i]);
                        EnemySpawn myEnemySpawn = newEnemy.GetComponent<EnemySpawn>();
                        if(myEnemySpawn != null) {
                            //Gives the enemy access to each of these variables, so that other scripts can reference them
                                myEnemySpawn.myCamera = myCamera;
                                myEnemySpawn.myPlayer = myPlayer;
                                myEnemySpawn.myTilemap = myTilemap;
                                myEnemySpawn.spawnTime = spawn;
                                myEnemySpawn.myManager = this;
                                myEnemySpawn.spawnTime = spawn;
                                myEnemySpawn.myEnemy = StartEnemyList[i];
                                myEnemySpawn.number = CurrentEnemyList.Count - 1;
                                Debug.Log(myEnemySpawn.myEnemy.ToString());
                                spawn +=spawnTimeIncrease;
                        }
                    }
                    else {
                        Transform newEnemy = Instantiate(StartEnemyList[i], myCamera.transform.position, Quaternion.Euler(0f, 0f, 0f));
                        CurrentEnemyList.Add(newEnemy);
                        CurrentEnemySpawner.Add(StartEnemySpawner[i]);
                        newEnemy.gameObject.layer = 8;
                        Enemy_HP myEnemyHP = newEnemy.GetComponent<Enemy_HP>();
                        if(myEnemyHP != null) {
                            //Gives the enemy access to each of these variables, so that other scripts can reference them
                            myEnemyHP.myCamera = myCamera;
                            myEnemyHP.myPlayer = myPlayer;
                            myEnemyHP.myTilemap = myTilemap;
                            myEnemyHP.spawnTime = spawn;
                            myEnemyHP.myManager = this;
                        }
                    }
                }
                roomReset = false;
            }
            else {
                //otherwise, enemies are created from the current enemy list
                float spawn = 0.01f;
                for(int i = 0; i < CurrentEnemyList.Count; i++) {
                    //Only creates the enemy if it really exists
                    if(CurrentEnemyList[i] != null) {
                        if(CurrentEnemySpawner[i]) {//If the current enemy uses a spawner, they get one
                            Transform newEnemy = Instantiate(EnemySpawnerPrefab, myCamera.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            CurrentEnemyList[i] = newEnemy;
                            EnemySpawn myEnemySpawn = newEnemy.GetComponent<EnemySpawn>();
                            if(myEnemySpawn != null) {
                            //Gives the enemy access to each of these variables, so that other scripts can reference them
                                myEnemySpawn.myCamera = myCamera;
                                myEnemySpawn.myPlayer = myPlayer;
                                myEnemySpawn.myTilemap = myTilemap;
                                myEnemySpawn.spawnTime = spawn;
                                myEnemySpawn.myManager = this;
                                myEnemySpawn.spawnTime = spawn;
                                myEnemySpawn.myEnemy = StartEnemyList[i];
                                myEnemySpawn.number = i;
                                spawn +=spawnTimeIncrease;
                            }
                        }
                        else {
                            Transform newEnemy = Instantiate(CurrentEnemyList[i], myCamera.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            CurrentEnemyList[i] = newEnemy;
                            newEnemy.gameObject.layer = 8;
                            Enemy_HP myEnemyHP = newEnemy.GetComponent<Enemy_HP>();
                            if(myEnemyHP != null) {
                            //Gives the enemy access to each of these variables, so that other scripts can reference them
                                myEnemyHP.myCamera = myCamera;
                                myEnemyHP.myPlayer = myPlayer;
                                myEnemyHP.myTilemap = myTilemap;
                                myEnemyHP.spawnTime = spawn;
                                myEnemyHP.myManager = this;
                            }
                        }
                    }
                }
            }
            //Some enemy types have additional considerations
            //Then, the room enter finishes
            roomEnter = false;
        }
        if(roomLeave) {
            //Have to do this for loop in the opposite direction, because some of these elements may be deleted
            for(int i = CurrentEnemyList.Count - 1; i >= 0; i--) {
                if(CurrentEnemyList[i] == null) {
                    CurrentEnemySpawner[i] = false;
                    //Do nothing;
                }
                else {
                    //Destroy the object, and then replaces it with the corresponding prefab
                    Destroy(CurrentEnemyList[i].gameObject);
                    CurrentEnemyList[i] = StartEnemyList[i];
                    CurrentEnemySpawner[i] = StartEnemySpawner[i];
                }
            }
            while(CurrentPickupList.Count > 0) {
                if(CurrentPickupList[0] != null ) {
                    Destroy(CurrentPickupList[0].gameObject);
                }
                CurrentPickupList.RemoveAt(0);
            }
            roomLeave = false;
        }
    }
}
