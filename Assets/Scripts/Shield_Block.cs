using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Block : MonoBehaviour
{
    public Sword_Behavior mySword;
    public bool isAttacking;
    public bool canBlock;
    
    void Update()
    {
        //check if is attacking
        if(Input.GetKey(KeyCode.X)){
            isAttacking = true;
        }
        else{
            isAttacking = false;
        }
        //This part detects if the player is hit by a rock and if the 
        //rock is blocked by the shield
    }
        void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.CompareTag("Rock")&& isAttacking = true)
       //{
            //get hit
        //}
        //if (col.gameObject.CompareTag("Rock")&& isAttacking = false)
        //{
            //check if the direction of rock match the facing of the player
        //}
    }
}
