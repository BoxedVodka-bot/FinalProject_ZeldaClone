using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moblin_Animation : MonoBehaviour
{
    Animator myAnimator; 
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        // I use basically the same thing for Moblin as Player
        // The keys are placeholders and we will replace them later in the script.
        // REMEBER: the conditions can be changed into anything, just make sure the scripts talk to each other
        // I only set up keys for testing
        if (Input.GetKey(KeyCode.I)){
                myAnimator.SetBool ("Walk_Back" , true);
            }
            else {
                myAnimator.SetBool ("Walk_Back" , false);
            }
        //Walking Left
        if (Input.GetKey(KeyCode.J)){
                myAnimator.SetBool ("Walk_Left" , true);
            }
            else {
                myAnimator.SetBool ("Walk_Left" , false);
            }
        //Walking Down
        if (Input.GetKey(KeyCode.K)){
                myAnimator.SetBool ("Walk_Front" , true);
            }
            else {
                myAnimator.SetBool ("Walk_Front" , false);
            }
        //Walking Right
        if (Input.GetKey(KeyCode.L)){
                myAnimator.SetBool ("Walk_Right" , true);
            }
            else {
                myAnimator.SetBool ("Walk_Right" , false);
            }
    }
}
