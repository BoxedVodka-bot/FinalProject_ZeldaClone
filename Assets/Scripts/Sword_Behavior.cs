using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Behavior : MonoBehaviour
{
    //i use these to decide which side the sword is facing
    public bool facingRight;
    public bool facingLeft;
    public bool facingUp;
    public bool facingDown;
    public bool canShootSword = true;
    public bool hasSword = true; //this can be adjusted. When getting the sword, this bool becomes true.
    public bool shootSword;
    Vector3 currentEulerAngles;
    float z;
    float moveSpeed = 0.06f;
    Vector2 swordPositionStart;

    void Start(){
        swordPositionStart = transform.position;
    }
    
    void Update()
    {
        //detect which side Link is facing, and rotate the sword to that direction
        if(Input.GetKey(KeyCode.W)){
            facingUp = true;
            facingDown = false;
            facingLeft = false;
            facingRight = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            Debug.Log("Up");
        }
        if(Input.GetKey(KeyCode.S)){
            facingUp = false;
            facingDown = true;
            facingLeft = false;
            facingRight = false;
            transform.localEulerAngles = new Vector3(0, 0, 180);
            Debug.Log("Down");
        }
        if(Input.GetKey(KeyCode.A)){
            facingUp = false;
            facingDown = false;
            facingLeft = true;
            facingRight = false;
            transform.localEulerAngles = new Vector3(0, 0, 90);
            Debug.Log("Left");
        }
        if(Input.GetKey(KeyCode.D)){
            facingUp = false;
            facingDown = false;
            facingLeft = false;
            facingRight = true;
            transform.localEulerAngles = new Vector3(0, 0, 270);
            Debug.Log("Right");
        }
        //when attack button is pressed, shoot the sword
        if(Input.GetKey(KeyCode.X)){
            shootSword = true;
        }
        //And also make the sword shoot to different directions
        if (shootSword == true && facingUp == true){
            GetComponent<Transform>().position += new Vector3(0f, moveSpeed, 0f);
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
        }
        if (shootSword == true && facingDown == true){
            GetComponent<Transform>().position += new Vector3(0f, moveSpeed*-1, 0f);
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
        }
        if (shootSword == true && facingLeft == true){
            transform.position += new Vector3(moveSpeed*-1, 0f, 0f);
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
        }
        if (shootSword == true && facingRight){
            GetComponent<Transform>().position += new Vector3(moveSpeed, 0f, 0f);
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
        }
        //hide the sword when not in use
        if (shootSword == false){
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        }
        //use raycast to detect stuff ahead
        float rayLength = 0.4f;
        Ray2D myRay = new Ray2D(transform.position, transform.up);
        Debug.DrawRay(myRay.origin, myRay.direction*rayLength, Color.white);   
        RaycastHit2D myRayHit = Physics2D.Raycast(myRay.origin, myRay.direction, rayLength);
        if (myRayHit.collider != null){
            Debug.Log("Hit something");
            //the sword disappear, return to the player's position
            shootSword = false;
            transform.position = swordPositionStart;
        }
        else{
            
        }
    }
}
