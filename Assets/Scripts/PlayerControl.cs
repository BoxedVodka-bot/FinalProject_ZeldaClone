﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //PURPOSE: Control player's movements
    //USAGE: put this on a player character
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator anim;
    //public Vector2 movement;
    public bool isWalking;

    //Collecting objects
    public int diamond = 0;
    public int key = 0;
    public int orb = 0;//Number of Bombs
    public int orb_slot;//The slot in the Inventory that bombs are in
    float collision_wait;//A weird check-in to wait between picking things up
    //Number counting
    public Text diamondNum;
    public Text keyNum;
    public Text orbNum;

    //for colliding w/walls
    private bool canMove = true;

    //for adjusting how far player bounce back when colliding with enemies
    public float force;
    public float maxForceTime;//How long the player can be pushed back for
    float curForceTime;//When this passes the max force time, the player will stop being pushed back
    
    //B Button interaction
    public B_Button myBButton;

    public HeartSystem myHearts;
    PlayerCombat myCombat;

    //Whether the player even can move
    public bool pause;
    public GameObject pauseCause;//What causes this to be paused
   
    private float x, y;
    public Vector3 directionRecord;

    [SerializeField] HeartSystem heartSystem;
    public bool invincibility;//whether the player is currently invincible
    public float invincibilityTime;
    public float maxInvincibilityTime;

    public AudioSource myAudioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myBButton = GetComponent<B_Button>();
        myCombat = GetComponent<PlayerCombat>();
        myHearts = GetComponent<HeartSystem>();
        //myAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {   
    if(!pause) {
        if(heartSystem.isDead == false){
            y = Input.GetAxisRaw("Vertical");
        }
        if (y == 0 && heartSystem.isDead == false){//Walking up & down has higher prio than walking left and right
            x = Input.GetAxisRaw("Horizontal"); //Gets a value from -1 to 1. -1 if left, 1 if right.
        }else{
            x = 0;
        }
        
        if(x !=0 || y != 0) {
            directionRecord = Vector3.Normalize(new Vector3(x, y, 0f));
        }

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
        if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
		}

    }
    if(invincibilityTime > 0) {
        //Needs to flash different colors (I think)
        invincibilityTime-=Time.deltaTime;
        if(invincibilityTime <= 0) {
            invincibilityTime = 0;
            invincibility = false;
            rb.velocity = new Vector2(0f, 0f);
            Unpause();
        }
    }
    //Outside of pause
        if(rb.velocity.magnitude > 0) {
            curForceTime+= Time.deltaTime;
            if(rb.velocity.magnitude > 0.1f) {
                //If being pushed back fast enough, still moves back
                pause = true;
                myCombat.pause = true;
                myBButton.pause = true;
                //Need to include something so that you gain invincibility for a little longer
            }
            else {
                //After it has diminished a certain amount, you reset
                rb.velocity = new Vector2(0f, 0f);
                pause = false;
                if(invincibilityTime == 0) {
                    Unpause();
                }
            }
           //Need to add something to stop you from moving between camera screens
        }
        if(curForceTime > 0 && rb.velocity.magnitude == 0) {
            pause = false;
            curForceTime = 0;
        }
        if(collision_wait > 0) {
            collision_wait -= Time.deltaTime;
            if(collision_wait < 0) {
                collision_wait = 0;
            }
        }
    }

    void Move(){
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);
        
        Ray2D myRay = new Ray2D(transform.position - transform.up *0.2f, directionRecord);
        float maxRayDist = 0.5f;
        if(directionRecord.y != 0) {
            maxRayDist = 0.35f;
            if(directionRecord.y == 1) {
                maxRayDist = 0.3f;
            }
        }
        LayerMask mask = LayerMask.GetMask("Wall");
        Debug.DrawRay(myRay.origin, myRay.direction*maxRayDist, Color.yellow);
        RaycastHit2D myRayHit = Physics2D.Raycast(myRay.origin, myRay.direction, maxRayDist, mask);
        RaycastHit2D myRayHit2 = Physics2D.Raycast(myRay.origin, myRay.direction, maxRayDist, mask);
        if(directionRecord.y!= 0) {
            float dif =0.25f;
            myRayHit = Physics2D.Raycast(myRay.origin + new Vector2(1f, 0f) * dif, myRay.direction, maxRayDist, mask);
            myRayHit2 = Physics2D.Raycast(myRay.origin - new Vector2(1f, 0f) * dif, myRay.direction, maxRayDist, mask);
            Debug.DrawRay(myRay.origin + new Vector2(1f, 0f) * dif, myRay.direction * maxRayDist, Color.yellow);
            Debug.DrawRay(myRay.origin - new Vector2(1f, 0f) * dif, myRay.direction * maxRayDist, Color.yellow);
        }
        else if(directionRecord.x!= 0) {
            float dif = 0.25f;
            myRayHit = Physics2D.Raycast(myRay.origin + new Vector2(0f, 1f) * dif/10f, myRay.direction, maxRayDist, mask);
            myRayHit2 = Physics2D.Raycast(myRay.origin - new Vector2(0f, 1f) * dif, myRay.direction, maxRayDist, mask);
            Debug.DrawRay(myRay.origin+ new Vector2(0f, 1f) * dif/10f, myRay.direction * maxRayDist, Color.yellow);
            Debug.DrawRay(myRay.origin - new Vector2(0f, 1f) * dif, myRay.direction * maxRayDist, Color.yellow);
        
        }
        if(myRayHit.collider == null && myRayHit2.collider == null){
            canMove = true;
        }else if((myRayHit.collider != null && myRayHit.collider.CompareTag("Wall")) || (myRayHit2.collider != null && myRayHit2.collider.CompareTag("Wall"))) {
            canMove = false;
        }
        if(canMove == true){
            transform.position += new Vector3(x, y, 0)*(Time.deltaTime*moveSpeed);
        }
    }
    
    //Player collide and collect items
    void OnTriggerEnter2D(Collider2D collision){
        if(collision_wait == 0) {
        if (collision.tag == "BlueRupee"){
            Destroy(collision.gameObject);
            diamond += 5;
            diamondNum.text = diamond.ToString();
            collision_wait +=0.1f;
        }
        if (collision.tag == "YellowRupee"){
            Destroy(collision.gameObject);
            diamond += 1;
            diamondNum.text = diamond.ToString();
            collision_wait +=0.1f;
        }
        if (collision.tag == "Bomb1"){
            Destroy(collision.gameObject);
            orb += 1;
            orbNum.text = orb.ToString();
            collision_wait +=0.1f;
            if(myBButton.equipped == 0) {
                myBButton.equipped = 1;
            }
        }
        if(collision.tag == "Heart") {
            Destroy(collision.gameObject);
            if(myHearts.curHealth < myHearts.maxHealth) {
                myHearts.curHealth += 2;
                if(myHearts.curHealth > myHearts.maxHealth) {
                    myHearts.curHealth = myHearts.maxHealth;
                }
                myHearts.checkHealthAmount();
            }
            collision_wait +=0.1f;
        }
        }
    }

    //Player knowckback when colliding with enemies
    //Player only knockback when there is health left
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemies" && heartSystem.curHealth != 0){
            myAudioSource.Play();
            Vector3 vectorFromMonsterTowardPlayer = transform.position - collision.gameObject.transform.position;
            vectorFromMonsterTowardPlayer.Normalize();
            Vector2 my2Dvector = new Vector2(vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y ); 
            rb.velocity += my2Dvector * force;
        }
    }
    void Unpause() {
            pause = false;
            myCombat.pause = false;
            myBButton.pause = false;
    }
}
