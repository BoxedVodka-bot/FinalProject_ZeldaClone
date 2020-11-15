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

    //Collecting objects
    public int diamond = 0;
    public int key = 0;
    public int orb = 0;

    //Number counting
    public Text diamondNum;
    public Text keyNum;
    public Text orbNum;

    //for colliding w/walls
    public bool canMove = true;


   
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
			//SceneManager.LoadScene( SceneManager.GetActiveScene().name );
		}

    }
    private void Move(){
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);
        

        Ray2D myRay = new Ray2D(transform.position + new Vector3(y, x, 0f) * (transform.localScale.x / 3), directionRecord);
        Ray2D myRay2 = new Ray2D(transform.position - new Vector3(y, x, 0f) * (transform.localScale.x / 3), directionRecord);
        float maxRayDist = 0.5f;
        Debug.DrawRay(myRay.origin, myRay.direction*maxRayDist, Color.yellow);
        RaycastHit2D myRayHit = Physics2D.Raycast(myRay.origin, myRay.direction, maxRayDist);
        Debug.DrawRay(myRay2.origin, myRay2.direction*maxRayDist, Color.yellow);
        RaycastHit2D myRayHit2 = Physics2D.Raycast(myRay2.origin, myRay2.direction, maxRayDist);
        if((myRayHit.collider != null && myRayHit.collider.CompareTag("Wall")) || (myRayHit2.collider != null && myRayHit2.collider.CompareTag("Wall"))) {
            //do nothing
        } 
        else {
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
        }
    }
}
