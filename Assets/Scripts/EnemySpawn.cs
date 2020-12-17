using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//PURPOSE: Used for enemies that spawn after a short amount of time, in a puff of smoke
//USAGE: Attached to a EnemySpawner Prefab, with a related animation
public class EnemySpawn : MonoBehaviour
{
    Animator myAnimator;
    public Camera myCamera;
    public Transform myPlayer;
    public Tilemap myTilemap;
    public float spawnTime;
    public RoomManager myManager;
    public Transform myEnemy;
    public int number;
    public int statBarOffest;//Offset of the statbar when spawning
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        List<Vector3> spawnPlaces = new List<Vector3>();
        //get a list of tiles this guy can spawn on
        //For loop of y values, than x values
        for(int i = 3; i < myCamera.orthographicSize * 2 - statBarOffest - 3f; i++) {//The -Xf is how far from the edge it will always be (at least)
            for(int j = 2; j < myCamera.orthographicSize * 2 * myCamera.aspect - 3f; j++ ) {
                //x position is equal to bottom left corner plus j
                int x = (int)(myCamera.transform.position.x - myCamera.orthographicSize * myCamera.aspect) + j; 
                //y position is equal to bottom left corner plus i
                int y = (int)(myCamera.transform.position.y - myCamera.orthographicSize) + i;
                //Check the tile at that position
                Tile myTile = myTilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                if(myTile == null) {
                    spawnPlaces.Add(new Vector3((float)x + 0.5f, (float)y, 0f));
                } 
            }
        }
        if(spawnPlaces.Count > 0) {
            int rnd = Random.Range(0, spawnPlaces.Count - 1);
            //This will need to be changed a bit so that the enemy doesn't spawn too close to the player
            transform.position = spawnPlaces[rnd];
        }
        myAnimator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTime > 0) {
            spawnTime -= Time.deltaTime;
            if(spawnTime <= 0) {
                myAnimator.speed = 1;
            }
        }
    }
    public void SpawnDeath() {
        Transform newEnemy = Instantiate(myEnemy, transform.position, transform.rotation);
        myManager.CurrentEnemyList[number] = newEnemy;
        newEnemy.gameObject.layer = 8;
        Enemy_HP myEnemyHP = newEnemy.GetComponent<Enemy_HP>();
        if(myEnemyHP != null) {
            //Gives the enemy access to each of these variables, so that other scripts can reference them
            myEnemyHP.myCamera = myCamera;
            myEnemyHP.myPlayer = myPlayer;
            myEnemyHP.myTilemap = myTilemap;
            myEnemyHP.myManager = myManager;
        }
        Destroy(this.gameObject);
    }
}
