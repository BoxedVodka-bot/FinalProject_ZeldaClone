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
    Vector2 movement;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //myAudioSource = GetComponent<AudioSource>();
    }
     void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //Gets a value from -1 to 1. -1 if left, 1 if right.
        movement.y = Input.GetAxisRaw("Vertical");

        //Animations for basic movements
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }
    void FixedUpdate(){
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
   
}
