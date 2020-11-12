using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //PURPOSE: Control player's movements
    //USAGE: put this on a player character
    public float moveSpeed = 0.05f;
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
    private bool moveUp = true;
    private bool moveDown = true;
    private bool moveLeft = true;
    private bool moveRight = true;

   
    private float x, y;
    public Vector3 directionRecord;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //myAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
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
    private void Move(){
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);

        //transform.Translate(x*Time.deltaTime*moveSpeed, y*Time.deltaTime*moveSpeed, 0);
        if(Input.GetKey(KeyCode.LeftArrow)){
            if(moveLeft == true){
                GetComponent<Transform>().position += new Vector3 (-0.05f, 0f, 0f);
            }
        }
        //If player press RIGHT ARROW, go right
        if(Input.GetKey(KeyCode.RightArrow)){
            if(moveRight == true){
                GetComponent<Transform>().position += new Vector3 (0.05f, 0f, 0f);
            }
        }
        //If player press UP ARRPW, go up
        if(Input.GetKey(KeyCode.UpArrow)){
            if(moveUp == true){
                GetComponent<Transform>().position += new Vector3 (0f, 0.05f, 0f);
            }
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            if(moveDown == true){
                GetComponent<Transform>().position += new Vector3 (0f, -0.05f, 0f);  
            } 
        }

        Ray2D myRay = new Ray2D(transform.position, Vector2.down);
        Ray2D myRay2 = new Ray2D(transform.position, Vector2.up);
        Ray2D myRay3 = new Ray2D(transform.position, Vector2.left);
        Ray2D myRay4 = new Ray2D(transform.position, Vector2.right);
        float maxRayDist = 0.6f;
        Debug.DrawRay(myRay.origin, myRay.direction*maxRayDist, Color.yellow);
        Debug.DrawRay(myRay2.origin, myRay2.direction*maxRayDist, Color.yellow);
        Debug.DrawRay(myRay3.origin, myRay3.direction*maxRayDist, Color.yellow);
        Debug.DrawRay(myRay4.origin, myRay4.direction*maxRayDist, Color.yellow);
        RaycastHit2D myRayHit = Physics2D.Raycast(myRay.origin, myRay.direction, maxRayDist);
        RaycastHit2D myRayHit2 = Physics2D.Raycast(myRay2.origin, myRay2.direction, maxRayDist);
        RaycastHit2D myRayHit3 = Physics2D.Raycast(myRay3.origin, myRay3.direction, maxRayDist);
        RaycastHit2D myRayHit4 = Physics2D.Raycast(myRay4.origin, myRay4.direction, maxRayDist);
        if(myRayHit.collider == null && myRayHit2.collider == null && myRayHit3.collider == null && myRayHit4.collider == null){
            moveDown = true;
            moveLeft = true;
            moveUp = true;
            moveRight = true;
        }else if(myRayHit.collider.CompareTag("Wall")){//down
            moveDown = false;
        }else if(myRayHit2.collider.CompareTag("Wall")){//up
            moveUp = false;
        }else if(myRayHit3.collider.CompareTag("Wall")){//left
            moveLeft = false;
        }else if(myRayHit4.collider.CompareTag("Wall")){//right
            moveRight = false;
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
        }
    }
}
