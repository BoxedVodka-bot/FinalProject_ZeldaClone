using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: Code attached to the player for whatever secondary item they have equipped
//USAGE: Attached to the player, needs to reference the inventory
public class B_Button : MonoBehaviour
{
    public int equipped;// 0 = nothing equipped; 1 = Bomb; 2 = Candle
    //KeyCode for whatever key ends up being the B Button
    public KeyCode myB_Button;
    public PlayerControl myPlayerControl;//Going to end up getting this from player movement
    public int charge;//For any items that require charges, this shows their charge amount
    public Transform myCandleFirePrefab;
    public Transform myBombPrefab;
    public Transform currentBomb;

    public bool pause;//Whether the B-pressing is paused
    //public Stats myStats;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if(!pause) {
        if(Input.GetKey(myB_Button)) {
            Vector3 direction = myPlayerControl.directionRecord;
            //Debug.Log(myPlayerControl.directionRecord.ToString());
            if(equipped == 1) {
                if(myPlayerControl.orb > 0) {
                    myPlayerControl.orb--;
                    if(myPlayerControl.orb == 0) {
                        equipped = 0;
                    }
                //Drop a bomb in front of the player
                    if(charge == 0) {
                        charge = 1;
                        //Creates the bomb in front of the player - the player remembers the bomb, bc they can only have 1 at a time
                        currentBomb = Instantiate(myBombPrefab, transform.position + direction, Quaternion.Euler(0f, 0f, 0f));
                    }
                }
            }
            else if(equipped == 2) {
                //Throws the fire from the blue Candle - once per room
                if(charge == 0) {
                    charge = 1; // Reset on room enter
                    Transform fire = Instantiate(myCandleFirePrefab, transform.position + direction, Quaternion.Euler(0f, 0f, 0f));
                    CandleFire fireFire = fire.GetComponent<CandleFire>();
                    fireFire.direction = direction;
                }
            }
        }
        if(equipped == 1 && charge == 1) {
            //You are able to place a new bomb every time your bomb is destroyed
            if(currentBomb == null) {
                charge = 0;
            }
        }
        }
    }
    }
}
