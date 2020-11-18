using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: create screen scrolling between each part of the map
//USAGE: put this on the main camera
public class CameraControl : MonoBehaviour
{
    public Transform target; //target pointing to the camera/player
    public Vector2 size; //size of the grid
    public float speed; //speed of transition. has to be a value between 0 and 1.
    public float statBarHeight;//Height of the menu stat bar
    public bool moving;//Used a checker to see if the camera is moving
    public float snapDist;//The distance from the correct position at which the camera snaps to the grid
    public List<RoomManager> myManagers = new List<RoomManager>();//List of manager objects for each room
    List<RoomManager> roomsEntered = new List<RoomManager>();
    public int maxRoomsEntered;//Number of different rooms the player enters before rooms begin resetting
    //managers only work if they are in the exact center of the room (same position as camera)

    //List of scripts which may need to be paused or referenced elsewhere
    PlayerControl myPlayerControl;
    B_Button myPlayerB;
    PlayerCombat myPlayerCombat;

    void Start() {
        myPlayerB = target.GetComponent<B_Button>();
        myPlayerControl = target.GetComponent<PlayerControl>();
        myPlayerCombat = target.GetComponent<PlayerCombat>();
    }

    void Update()
    {
        //check if player position is within the size & move the camera
        Vector3 pos = new Vector3( Mathf.RoundToInt(target.position.x / size.x) * size.x, Mathf.RoundToInt(target.position.y / size.y) * size.y, transform.position.z);
        //What it needs to be:
        //x min = camera x min; x-max = camera-x-max; y-min=camera-y-min;y-max = camera-y-max - statBarHeight
        pos = new Vector3( Mathf.RoundToInt(target.position.x / size.x) * size.x, Mathf.RoundToInt((target.position.y + statBarHeight / 2) / size.y) * size.y, transform.position.z);
        if(pos != transform.position && !moving) {
            //Get current room manager so that enemies can be deleted
            myPlayerControl.pause = true;
            myPlayerB.pause = true;
            myPlayerCombat.pause = true;
            moving = true;
            for(int i = 0; i < myManagers.Count; i++) {
                //If this manager is in the bounds of the screen
                if(myManagers[i].transform.position.x == transform.position.x && myManagers[i].transform.position.y == transform.position.y) {
                    myManagers[i].roomLeave = true;
                }
            }
        }
        //make the transition smoother
        transform.position = Vector3.Lerp(transform.position, pos, speed);
        //Next, if the position is close enough to the target, it snaps into place, and enemies spawn
        if(moving) {
            if((transform.position.x > pos.x - snapDist && transform.position.x < pos.x + snapDist) &&(transform.position.y > pos.y - snapDist && transform.position.y < pos.y + snapDist)) {
                moving = false;
                myPlayerControl.pause = false;
                myPlayerB.pause = false;
                myPlayerCombat.pause = false;
                if(myPlayerB.equipped == 1) {
                    if(myPlayerB.currentBomb != null) {
                        Destroy(myPlayerB.currentBomb.gameObject);
                    }
                }
                else if(myPlayerB.equipped == 2) {
                    myPlayerB.charge = 0;
                }
                transform.position = pos;
                //Needs to get the new current room manager
                for(int i = 0; i < myManagers.Count; i++) {
                //If this manager is in the bounds of the screen
                    if(myManagers[i].transform.position.x == transform.position.x && myManagers[i].transform.position.y == transform.position.y) {
                        myManagers[i].roomEnter = true;
                        Debug.Log(myManagers[i].name);
                        if(myManagers[i].roomReset == true) {
                            //This room reset code currently doesn't work
                            roomsEntered.Add(myManagers[i]);
                            if(roomsEntered.Count > maxRoomsEntered) {
                                roomsEntered[0].roomReset = false;
                                roomsEntered.RemoveAt(0);
                            }
                        }
                    }
                }
            }
        }
    }
}
