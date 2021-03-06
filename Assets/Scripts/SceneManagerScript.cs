﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static int RoomNumber;
    public static int lastRoom;
    public NPCSpawnerScript NPCSpawn;
    public ItemSpawnerScript ItemSpawn;
    public Camera myCamera;
    //public GameObject PlayerObject;
    public Transform myPlayer;
    public Transform npcRoomStart;
    public bool PlayerSpawned;
    public bool enterRoom;
    public bool leaveRoom;
    public int newRoomNumber;
    public bool InShop;
    public PickUp ItemPickUp;
    public List<Transform> npcRoomExit = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        //newRoomNumber = RoomNumber;
        // eventually create a list of roomnumbers here
        PlayerSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSpawned == false && enterRoom){
            //myPlayer = Instantiate(PlayerObject.transform, new Vector3 (0, -5.5f, 0), Quaternion.identity);
            if(RoomNumber != 0) {
                InShop = true;
                myPlayer.position = npcRoomStart.position;
                myCamera.transform.position = new Vector3(transform.position.x, transform.position.y, myCamera.transform.position.z);
            }
            else {
                if(ItemPickUp != null) {
                    //ItemPickUp.cost = 0;
                    Destroy(ItemPickUp.gameObject);
                }
                InShop = false;
                myPlayer.position = npcRoomExit[lastRoom - 1].position;
                CameraControl CamControl = myCamera.GetComponent<CameraControl>();
                myCamera.transform.position = new Vector3( Mathf.RoundToInt(CamControl.target.position.x / CamControl.size.x) * CamControl.size.x, Mathf.RoundToInt(CamControl.target.position.y / CamControl.size.y) * CamControl.size.y, myCamera.transform.position.z);
                //Also has to deal with reinstating enemies
                for(int i = 0; i < CamControl.myManagers.Count; i++) {
                //If this manager is in the bounds of the screen
                    if(CamControl.myManagers[i].transform.position.x == CamControl.transform.position.x && CamControl.myManagers[i].transform.position.y == CamControl.transform.position.y) {
                        CamControl.myManagers[i].roomEnter = true;
                        Debug.Log(CamControl.myManagers[i].name);
                        if(CamControl.myManagers[i].roomReset == true) {
                            //This room reset code currently doesn't work
                            CamControl.roomsEntered.Add(CamControl.myManagers[i]);
                            if(CamControl.roomsEntered.Count > CamControl.maxRoomsEntered) {
                                CamControl.roomsEntered[0].roomReset = true;
                                CamControl.roomsEntered.RemoveAt(0);
                            }
                        }
                    }
                }
                NPCSpawn.NPCNumber = 0;
                NPCSpawn.NPCSpawned = false;
                ItemSpawn.ItemSequenceNumber = 0;
            }
            PlayerSpawned = true;
            if (RoomNumber == 1){
                NPCSpawn.NPCNumber = 1;
                ItemSpawn.ItemSequenceNumber = 1;
                // sword room
            }
            if (RoomNumber == 2){
                // candle room
                NPCSpawn.NPCNumber = 2;
                ItemSpawn.ItemSequenceNumber = 2;
            }
            if (RoomNumber == 3){
                // bomb room
                NPCSpawn.NPCNumber = 3;
                ItemSpawn.ItemSequenceNumber = 3;
            }
            if (RoomNumber == 4){
                // candle room
                NPCSpawn.NPCNumber = 4;
                ItemSpawn.ItemSequenceNumber = 4;
            }
            if (RoomNumber == 5){
                // health room
                NPCSpawn.NPCNumber = 5;
                ItemSpawn.ItemSequenceNumber = 5;
            }
            ItemSpawn.ItemsSpawned = false;
            NPCSpawn.NPCSpawned = false;
            enterRoom = false;
        }
        else if(leaveRoom) {
            //Go to next room
            //Needs to list various variables as statics so they can be maintained between rooms
            if(newRoomNumber == 0) {//Goes back to open room
                lastRoom = RoomNumber;
                RoomNumber = 0;
            }
            else {//Goes to NPC room
                lastRoom = RoomNumber;
                RoomNumber = newRoomNumber;
            }
            enterRoom = true;
            leaveRoom = false;
            PlayerSpawned = false;
            CameraControl CamControl = myCamera.GetComponent<CameraControl>();
                for(int i = 0; i < CamControl.myManagers.Count; i++) {
                    if(CamControl.myManagers[i].transform.position.x == CamControl.transform.position.x && CamControl.myManagers[i].transform.position.y == CamControl.transform.position.y) {
                        CamControl.myManagers[i].roomLeave = true;
                }
            }
        }
        // if room number = x , spawn the corresponding x npc and items
        // if you have the sword, don't spawn the sword again
        
    }
}
