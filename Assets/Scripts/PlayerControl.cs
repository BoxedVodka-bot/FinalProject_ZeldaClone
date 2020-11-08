using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    //PURPOSE: Control player's movements
    //USAGE: put this on a player character
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    //public Vector2 movement;
    private bool isWalking;
    public float x, y;

    //Collecting objects
    public int diamond = 0;
    public int key = 0;
    public int orb = 0;

    //Number counting
    public Text diamondNum;
    public Text keyNum;
    public Text orbNum;

   
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
    
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Diamond"){
            Destroy(collision.gameObject);
            diamond += 1;
            diamondNum.text = diamond.ToString();
        }
        if (collision.tag == "Key"){
            Destroy(collision.gameObject);
            key += 1;
            keyNum.text = key.ToString();
        }
        if (collision.tag == "Orb"){
            Destroy(collision.gameObject);
            orb += 1;
            orbNum.text = orb.ToString();
        }
    }
}
