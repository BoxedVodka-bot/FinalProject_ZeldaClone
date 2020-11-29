using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Block : MonoBehaviour
{
    public Sword_Behavior mySword;
    public bool isAttacking;
    public bool bulletFromLeft;
    public bool bulletFromRight;
    public bool bulletFromUp;
    public bool bulletFromDown;
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
        //if not, check if the player is facing towards the bullet's direction.
        //we do not have bullets at this moment right?
        //anyway this part is for checking the match.
        if(isAttacking == false){
            if(mySword.facingDown){
                canBlock = true;
            }
            if(mySword.facingUp){
                canBlock = true;
            }
            if(mySword.facingLeft){
                canBlock = true;
            }
            if(mySword.facingRight){
                canBlock = true;
            }
        }
    }
}
