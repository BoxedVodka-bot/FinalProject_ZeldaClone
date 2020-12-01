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
    public bool hasSword;// = true; //this can be adjusted. When getting the sword, this bool becomes true.
    public bool shootSword;
    public HeartSystem myHearts;
    public PlayerControl myPlayer;
    Vector3 currentEulerAngles;
    float z;
    public float moveSpeed;
    Vector2 swordPositionStart;
    public Camera myCamera;
    public float statBarOffset;
    public Vector3 direction;//direction the sword supposed to go in

    void Start(){
        swordPositionStart = transform.position;
    }
    
    void Update()
    {
        //detect which side Link is facing, and rotate the sword to that direction
        
        //when attack button is pressed, shoot the sword
        if(Input.GetKey(KeyCode.X)){
            if(hasSword && myHearts.curHealth == myHearts.maxHealth) {
                //shootSword = true;
            }
        }
        //And also make the sword shoot to different directions
        if(shootSword) {
            transform.position += moveSpeed * Time.deltaTime * direction;
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
            float rayLength = 0.4f;
            //use raycast to detect stuff ahead - needs to specifically hit enemies
            LayerMask enemyLayer = LayerMask.GetMask("Enemies");
            Ray2D myRay = new Ray2D(transform.position, transform.up);
            Debug.DrawRay(myRay.origin, myRay.direction*rayLength, Color.white);   
            RaycastHit2D myRayHit = Physics2D.Raycast(myRay.origin, myRay.direction, rayLength, enemyLayer);
            if (myRayHit.collider != null){
                Debug.Log("Hit something");
                //the sword disappear, return to the player's position
                //need to add animation
                if(myRayHit.collider.CompareTag("Enemies")) {
                    Enemy_HP enemyHP = myRayHit.collider.GetComponent<Enemy_HP>();
                    shootSword = false;
                    enemyHP.TakeDamage(-1, true, myPlayer.directionRecord);
                    transform.position = myPlayer.transform.position;
                }
            }
            //Also needs to be stop if it gets too close to the edge of the screen
            else {
                //Each direction has its own test to see if it goes off screen
                if(direction.x > 0) {
                    if(transform.position.x > myCamera.transform.position.x + myCamera.aspect * myCamera.orthographicSize - 1f) {
                        shootSword = false;
                        transform.position = myPlayer.transform.position;
                    }
                }
                else if(direction.x < 0) {
                    if(transform.position.x < myCamera.transform.position.x - myCamera.aspect * myCamera.orthographicSize + 1f) {
                        shootSword = false;
                        transform.position = myPlayer.transform.position;
                    }
                }
                else if(direction.y > 0) {
                    if(transform.position.y > myCamera.transform.position.y + myCamera.orthographicSize - statBarOffset) {
                        shootSword = false;
                        transform.position = myPlayer.transform.position;
                    }
                }
                else if(direction.y < 0)  {
                    if(transform.position.y < myCamera.transform.position.y - myCamera.orthographicSize) {
                        shootSword = false;
                        transform.position = myPlayer.transform.position;
                    }
                }
            }
        }
        //hide the sword when not in use
        if (shootSword == false){
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        }
       
        else{
            
        }
    }
    public void ThrowSword() {
        if(myHearts.curHealth == myHearts.maxHealth && !shootSword) {
            shootSword = true;
            direction = myPlayer.directionRecord;
            transform.position = myPlayer.transform.position + direction;
            if(direction.y > 0) {
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            else if(direction.y < 0) {
                transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            }
            else if(direction.x > 0) {
                transform.localEulerAngles = new Vector3(0f, 0f, 270f);
            }
            else {
                transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            }

        }
    }
    void Unused() {
        if (shootSword == true && facingUp == true){
            transform.position += new Vector3(0f, moveSpeed, 0f);
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
        }
        if (shootSword == true && facingDown == true){
            transform.position += new Vector3(0f, moveSpeed*-1, 0f);
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
        }
        if (shootSword == true && facingLeft == true){
            transform.position += new Vector3(moveSpeed*-1, 0f, 0f);
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
        }
        if (shootSword == true && facingRight){
            transform.position += new Vector3(moveSpeed, 0f, 0f);
            canShootSword = false;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
        }
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !shootSword){
            facingUp = true;
            facingDown = false;
            facingLeft = false;
            facingRight = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            Debug.Log("Up");
        }
        if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && !shootSword){
            facingUp = false;
            facingDown = true;
            facingLeft = false;
            facingRight = false;
            transform.localEulerAngles = new Vector3(0, 0, 180);
            Debug.Log("Down");
        }
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !shootSword){
            facingUp = false;
            facingDown = false;
            facingLeft = true;
            facingRight = false;
            transform.localEulerAngles = new Vector3(0, 0, 90);
            Debug.Log("Left");
        }
        if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !shootSword){
            facingUp = false;
            facingDown = false;
            facingLeft = false;
            facingRight = true;
            transform.localEulerAngles = new Vector3(0, 0, 270);
            Debug.Log("Right");
        }
    }
}
