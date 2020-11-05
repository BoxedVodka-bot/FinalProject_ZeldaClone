using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    Animator myAnimator; 
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        //Decide which way Link is walking to
        //Walking up
        if (Input.GetKey(KeyCode.W)){
                myAnimator.SetBool ("Walk_Back" , true);
                Debug.Log("walk back!!");
            }
            else {
                myAnimator.SetBool ("Walk_Back" , false);
            }
        //Walking Left
        if (Input.GetKey(KeyCode.A)){
                myAnimator.SetBool ("Walk_Left" , true);
            }
            else {
                myAnimator.SetBool ("Walk_Left" , false);
            }
        //Walking Down
        if (Input.GetKey(KeyCode.S)){
                myAnimator.SetBool ("Walk_Front" , true);
            }
            else {
                myAnimator.SetBool ("Walk_Front" , false);
            }
        //Walking Right
        if (Input.GetKey(KeyCode.D)){
                myAnimator.SetBool ("Walk_Right" , true);
            }
            else {
                myAnimator.SetBool ("Walk_Right" , false);
            }
        //If Link is not walking at all
        if (!Input.anyKey){
                myAnimator.SetBool ("Idle" , true);
        }
        else{
                myAnimator.SetBool ("Idle" , false);
            }
        
    }
}
