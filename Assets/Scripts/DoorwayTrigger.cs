using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: When the player enters this, they are transported to the corresponding room
//USAGE: Attached to each doorway (of any sort) (prefabs?)
public class DoorwayTrigger : MonoBehaviour
{
    public SceneManagerScript mySceneManager;//Scenemanager that stays around for the next room
    public int myRoomNumber;//Corresponding room number for this doorway, used with scenemanager
    
    void OnTriggerEnter2D(Collider2D activator) {
        //If the player is what enters, 
        if(activator.CompareTag("Player")) {
            //The sceneManager's room number value becomes equal to this trigger's value
            //Go to the NPC scene room
           // mySceneManager.newRoomNumber = myRoomNumber;
        }
    }
}
