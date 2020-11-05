using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //PURPOSE: Control player's movements
    //USAGE: put this on a player character
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    //public Vector2 movement;
    private bool isWalking;
    private float x, y;


   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //myAudioSource = GetComponent<AudioSource>();
    }
     void Update()
    {
        x = Input.GetAxisRaw("Horizontal"); //Gets a value from -1 to 1. -1 if left, 1 if right.
        y = Input.GetAxisRaw("Vertical");

        if(x != 0 || y!= 0){
            if(!isWalking){
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
            }
            
            Move();
        }else{
            if (isWalking){
                isWalking = false;
                anim.SetBool("isWalking", isWalking);
        }

        }
    }
    private void Move(){
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);

        transform.Translate(x*Time.deltaTime*moveSpeed, y*Time.deltaTime*moveSpeed, 0);
    }
    

}
