using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //PURPOSE: Control player's movements
    //USAGE: put this on a player character
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    //public Vector2 movement;
    private bool isWalking;

    //Collecting objects
    public int diamond = 0;
    public int key = 0;
    public int orb = 0;

    //Number counting
    public Text diamondNum;
    public Text keyNum;
    public Text orbNum;

    //for colliding w/walls
    private bool canMove = true;

    //B Button interaction
    public B_Button myBButton;

    //Whether the player even can move
    public bool pause;
   
    private float x, y;
    public Vector3 directionRecord;
    public bool pause;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //myAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {   
    if(!pause) {
        y = Input.GetAxisRaw("Vertical");
        if (y == 0){
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
    }
    private void Move(){
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);
        

        Ray2D myRay = new Ray2D(transform.position - transform.up *0.2f, directionRecord);
        float maxRayDist = 0.5f;
        if(directionRecord.y == -1) {
            maxRayDist = 0.4f;
        }
        Debug.DrawRay(myRay.origin, myRay.direction*maxRayDist, Color.yellow);
        RaycastHit2D myRayHit = Physics2D.Raycast(myRay.origin, myRay.direction, maxRayDist);
        if(myRayHit.collider == null){
            canMove = true;
        }else if(myRayHit.collider.CompareTag("Wall")){
            canMove = false;
        }
        if(canMove == true){
            transform.position += new Vector3(x, y, 0)*(Time.deltaTime*moveSpeed);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "BlueRupee"){
            Destroy(collision.gameObject);
            diamond += 1;
            diamondNum.text = diamond.ToString();
        }
        if (collision.tag == "YellowRupee"){
            Destroy(collision.gameObject);
            key += 1;
            keyNum.text = key.ToString();
        }
        if (collision.tag == "Bomb"){
            Destroy(collision.gameObject);
            orb += 1;
            orbNum.text = orb.ToString();
            if(myBButton.equipped == 0) {
                myBButton.equipped = 1;
            }
        }
    }
}
