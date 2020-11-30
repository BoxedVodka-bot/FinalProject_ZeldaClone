using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: Movement code for the Octoroks (rock shooty octopi)
//USAGE: Attached to Octorok Prefab
public class Enemy_Octorok : MonoBehaviour
{
    public float speed;
    int octorokHealth;
    public bool strongVersion;
    Enemy_HP myHP;
    public float counter;
    public float max_counter;
    public bool straight;
    public float timeStraight;
    public float max_timeStraight;
    public float timeStopped;
    public float max_timeStopped;
    public float timeShooting;
    public float max_timeShooting;
    public bool moving;
    public bool stopped;
    public bool shooting;
    public int rockShot;

    public Ray2D frontDetect;
    public Ray2D leftDetect;
    public Ray2D rightDetect;
    public RaycastHit2D hitLeft;
    public RaycastHit2D hitRight;
    public RaycastHit2D hitFront;
    public float rayDist;//Distance of all rays
    public Rigidbody2D octorokRB;
    public float rnd;
    public int modeSwitcher;

    Camera myCamera;
    public GameObject rock;

    // Start is called before the first frame update
    void Start()
    {
        myHP = GetComponent<Enemy_HP>();
        myCamera =myHP.myCamera;
        octorokRB = GetComponent<Rigidbody2D>();
        counter = 0;
        timeStraight = Random.Range(0f, max_timeStraight / 2f);
        timeShooting = 0;
        straight = true;
        moving = true;
        modeSwitcher = 0;
        rockShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //cast rays in front, to the left, and to the right of the octorok
        // right ray
        LayerMask mask = LayerMask.GetMask("Wall");
        hitRight = Physics2D.Raycast(transform.position, transform.right, rayDist * 2f, mask);
        Debug.DrawRay(transform.position, transform.right * rayDist * 2f, Color.red);
        // left ray
        hitLeft = Physics2D.Raycast(transform.position, -transform.right, rayDist *2f, mask);
        Debug.DrawRay(transform.position, -transform.right*rayDist *2f, Color.blue);
        // front ray
        hitFront = Physics2D.Raycast(transform.position, transform.up, rayDist, mask);
        Debug.DrawRay(transform.position, transform.up * rayDist, Color.green);
        
        //go straight if in the "straight" state
        if (straight && moving)
        {
            //At the start of movement, it needs to check to see if its going to go off the camera, in which case it turns
            if(transform.up.x > 0) {
                if((transform.position + transform.up).x  > myCamera.transform.position.x + myCamera.orthographicSize * myCamera.aspect - 1f) {
                    straight = false;
                }
            }
            else if(transform.up.x < 0) {
                if((transform.position + transform.up).x  < myCamera.transform.position.x - myCamera.orthographicSize * myCamera.aspect + 1f) {
                    straight = false;
                }
            }
            else if(transform.up.y > 0) {
                if((transform.position + transform.up).y  > myCamera.transform.position.y + myCamera.orthographicSize - 2.5f) {
                    straight = false;
                }
            }
            else if(transform.up.y < 0) {
                if((transform.position + transform.up).y  < myCamera.transform.position.y - myCamera.orthographicSize + 0.5f) {
                    straight = false;
                    Debug.Log("UP");
                }
            }
            //transform.Translate(transform.up * speed * Time.deltaTime);
            transform.position += transform.up * speed * Time.deltaTime;
            timeStraight+=Time.deltaTime;

            if (hitFront.collider != null)
            {
                straight = false;
            }
            if(!straight) {//After all is done, if it has decided to turn, its straight timer resets
                timeStraight = Random.Range(0f, max_timeStraight - 1f);
            }
        }
        else if (!straight && moving)
        {
            if (hitRight.collider == null && hitLeft.collider != null)
            {
                //straight = false;
                Debug.Log("turned right");
                transform.Rotate(new Vector3(0f, 0f, -90f));
                straight = true;
                moving = true;
            }
            else if (hitLeft.collider == null && hitRight.collider != null)
            {
                //straight = false;
                Debug.Log("turned left");
                transform.Rotate(new Vector3(0f, 0f, 90f));
                straight = true;
                moving = true;
            }
            else if (hitLeft.collider == null && hitLeft.collider == null)
            {
                rnd = Random.Range(0f, 1f);
                if (rnd >= .5f)
                {
                    Debug.Log("turned right");
                    transform.Rotate(new Vector3(0f, 0f, -90f));

                }
                if (rnd < .5f)
                {
                    Debug.Log("turned left");
                    transform.Rotate(new Vector3(0f, 0f, 90f));
                }
                straight = true;
                moving = true;
            }
            else {
                //Cautionary measure in case an enemy gets stuck trying to turn between 2 walls
                timeStraight -= 0.5f;
            }
        }

        //if straight too long change directions
        if (timeStraight >= max_timeStraight)
        {
            straight = false;
            timeStraight = 0;
            Debug.Log("not straight no more");
            // if the time going straight is too long roll to see if it stops, turns, or shoots
            modeSwitcher = Random.Range(1, 4);
            Debug.Log(modeSwitcher);
        }

        if (modeSwitcher == 1)
        {
            //waiting mode
            stopped = true;
            moving = false;
            shooting = false;
            modeSwitcher = 0;
        }
        else if (modeSwitcher == 2)
        {
            //moving mode
            moving = true;
            stopped = false;
            shooting = false;
            modeSwitcher = 0;
        }
        else if (modeSwitcher == 3)
        {
            //shooting mode
            shooting = true;
            stopped = false;
            moving = false;
            modeSwitcher = 0;
        }

        //shooting mode
        if (shooting && timeShooting <= max_timeShooting)
        {
            //spawn a rock, increment counter
            if (rockShot < 1)
            {
                Instantiate(rock, transform.position, transform.rotation);
                rockShot++;
            }
            
            timeShooting++;
        }
        else if (shooting && timeShooting > max_timeShooting)
        {
            //go back to moving after shooting
            shooting = false;
            stopped = true;
            moving = false;
            straight = true;
            timeStraight = 0;
            timeShooting = 0;
            rockShot = 0;
        }

        //stopped mode
        if (stopped && timeStopped <= max_timeStopped)
        {
            timeStopped++;
        }
        else if (stopped && timeStopped > max_timeStopped)
        {
            shooting = false;
            stopped = false;
            moving = true;
            straight = false;
            timeStraight = 0;
            timeStopped = 0;
        }


        

        //if (hitRight.collider == null)
        //{
        //    rnd = Random.Range(0f, 1f);
        //    if (rnd >= .5f)
        //    {
        //        straight = true;
        //    }
        //    else
        //    {
        //        straight = false;
        //    }
        //}

        //if (hitLeft.collider == null)
        //{
        //    rnd = Random.Range(0f, 1f);
        //    if (rnd >= .5f)
        //    {
        //        straight = true;
        //    }
        //    else
        //    {
        //        straight = false;
        //    }
        //}

        //if (hitFront.collider != null)
        //{
        //    straight = false;

        //}


    }
}
