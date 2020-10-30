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
    //public
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(myB_Button)) {
            if(equipped == 1) {
                //Drop a bomb in front of the player
            }
            else if(equipped == 2) {
                //Throws the fire from the blue Candle
            }
        }
    }
}
