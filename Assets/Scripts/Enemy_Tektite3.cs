using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//PURPOSE: A third attempt at trying to get the Tektite movement to work
public class Enemy_Tektite3 : MonoBehaviour
{
     bool jumping;//Bool that says whethere enemy is jumping
    public float speed;//Speed of the Tektite while jumping
    public float fallSpeed;
    float timeToJump;
    float jumpTime;
    public float jumpChance;//Chance the enemy will actually jump
    public float maxTimeToJump;
    public float maxJumpTime;
    Camera myCamera;
    Enemy_HP myHP;
    Transform myPlayer;
    Vector3 jumpDirection;
    public Vector3 baseJumpDirection;
    Tilemap myTilemap;
    public int statBarOffest;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        timeToJump = Random.Range(0f, maxTimeToJump);
        myHP = GetComponent<Enemy_HP>();
        myCamera = myHP.myCamera;
        myPlayer = myHP.myPlayer;
        myTilemap = myHP.myTilemap;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(jumping) {
            jumpTime -= Time.deltaTime;
            if(jumpTime < 0) {
                jumping = false;
                myAnimator.speed = 1;
                Debug.Log(jumpDirection.y.ToString());
            }
            else {
                transform.position += jumpDirection * Time.deltaTime * speed;
                jumpDirection += new Vector3(0f, -1f, 0f) *Time.deltaTime * fallSpeed;
            }
        }
    }
    void Jump() {
        timeToJump = Random.Range(0f, maxTimeToJump);
        jumping = true;
        jumpTime = Random.Range(maxJumpTime / 4f, maxJumpTime);
        float rnd = Random.Range(0f, 1f);
        if(rnd < 0.5f) {
            jumpDirection = baseJumpDirection.normalized;
        }
        else {
            jumpDirection = new Vector3(-baseJumpDirection.x, baseJumpDirection.y, 0f).normalized;
        }
        //Now need to add clauses to make sure it doesn't jump off of the screen
        //If closer to the edge of the screen then 2 blocks away
        if(transform.position.x > myCamera.transform.position.x + myCamera.aspect *myCamera.orthographicSize - 2f) {
            jumpDirection = new Vector3(-baseJumpDirection.x, baseJumpDirection.y, 0f).normalized;
        }
        else if(transform.position.x < myCamera.transform.position.x - myCamera.aspect *myCamera.orthographicSize + 2f) {
            jumpDirection = baseJumpDirection.normalized;
        }
        if(transform.position.y < myCamera.transform.position.y - myCamera.orthographicSize + 2f) {
            jumpTime = maxJumpTime / 2f;
            jumpDirection += new Vector3(0f, 1f, 0f);
        }
        else if(transform.position.y > myCamera.transform.position.y + myCamera.orthographicSize - statBarOffest - 2f) {
            jumpTime = maxJumpTime;
        }

    }
     void chanceToJump() {//This checks to see if the enemy wants to jump, and then it might actually jump
        //Eventually, will need to add in a thing where it pauses in the down position before jumping
        float rnd = Random.Range(0f, 1f);
        if(rnd <= jumpChance) {
            myAnimator.speed = 0;
            Jump();
        }
    }
}
