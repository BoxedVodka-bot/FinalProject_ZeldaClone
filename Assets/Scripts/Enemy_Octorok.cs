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
    //Time stopped - should only happen when going to shoot
    public float timeStopped;
    public float max_timeStopped;
    public float timeShooting;
    public float max_timeShooting;
    public bool moving;
    public bool stopped;
    public float timeToShoot;
    public float maxTimeToShoot;
    public bool shooting;
    bool turn;
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
    public float modeSwitcher;
    public float directionPoint;
    Camera myCamera;
    public GameObject rock;
    public GameObject currentRock;
    bool cameraTurnCause;//Boolean used if camera is the player's reason for turning

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
        int random = Random.Range(0,4);
        transform.localEulerAngles = new Vector3(0f, 0f, 90f * random);
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
        
        //Using this because transform.up doesn't seem to always work
        Vector3 direction = new Vector3(0f, 0f, 0f);
        if(Mathf.Round(transform.eulerAngles.z) == 0f) {
            direction = new Vector3(0f, 1f, 0f);
        }
        else if(Mathf.Round(transform.eulerAngles.z) == 90f) {
            direction = new Vector3(-1f, 0f, 0f);
        }
        else if(Mathf.Round(transform.eulerAngles.z) == 180f) {
            direction = new Vector3(0f, -1f, 0f);
        }
        else if(Mathf.Round(transform.eulerAngles.z) == 270f) {
            direction = new Vector3(1f, 0f, 0f);
        }
        //go straight if in the "straight" state
        if (straight && moving)
        {
            directionPoint = 0;//A checking variable to see if 
            //At the start of movement, it needs to check to see if its going to go off the camera, in which case it turns
            //Currently 2 issues: 1) doesn't shoot correct amount, 2) still a little glitchy
            if (direction.x == 1f) {
                directionPoint = Mathf.Ceil(transform.position.x -0.5f) +0.5f;
                if ((transform.position + direction).x  > myCamera.transform.position.x + myCamera.orthographicSize * myCamera.aspect - 0.5f) {
                    straight = false;
                    cameraTurnCause = true;
                    turn = true;
                }
            }
            else if (direction.x == -1f) {
                directionPoint = Mathf.Floor(transform.position.x -0.5f) +0.5f;
                if ((transform.position + direction).x  < myCamera.transform.position.x - myCamera.orthographicSize * myCamera.aspect + 0.5f) {
                    straight = false;
                    cameraTurnCause = true;
                    turn = true;
                }
            }
            else if (direction.y == 1f) {
                directionPoint = Mathf.Ceil(transform.position.y);
                if ((transform.position + direction).y  > myCamera.transform.position.y + myCamera.orthographicSize - 2.5f) {
                    straight = false;
                    cameraTurnCause = true;
                    turn = true;
                }
            }
            else if (direction.y == -1f) {
                directionPoint = Mathf.Floor(transform.position.y);
                if ((transform.position + direction).y  < myCamera.transform.position.y - myCamera.orthographicSize + 0.5f) {
                    straight = false;
                    cameraTurnCause = true;
                    turn = true;
                }
            }
            //transform.Translate(transform.up * speed * Time.deltaTime);
            //Ok, right here, I'm trying something out: compare position pre-move with post-move - if moves through a block's center, it may go back to that block's center and might turn
            transform.position += transform.up * speed * Time.deltaTime;
            if(direction.x == 1) {
                //Using Epsilon in case the enemy lands exactly on a square
                if(Mathf.Ceil(transform.position.x-0.5f) +0.5f > directionPoint) {
                    checkTurn(direction, new Vector3(directionPoint, transform.position.y, transform.position.z));
                }
            }
            else if(direction.x == -1) {
                if(Mathf.Floor(transform.position.x-0.5f) +0.5f < directionPoint) {
                    checkTurn(direction, new Vector3(directionPoint, transform.position.y, transform.position.z));
                }
            }
            else if(direction.y == 1) {
                if(Mathf.Ceil(transform.position.y) > directionPoint) {
                    checkTurn(direction, new Vector3(transform.position.x, directionPoint, transform.position.z));
                }
            }
            else if(direction.y == -1) {
                if(Mathf.Floor(transform.position.y) < directionPoint) {
                    checkTurn(direction, new Vector3(transform.position.x, directionPoint, transform.position.z));
                }
            }
            timeStraight+=Time.deltaTime;

            if (hitFront.collider != null)
            {
                turn = true;
                straight = false;
            }
            if (!straight) {//After all is done, if it has decided to turn, its straight timer resets
                timeStraight = Random.Range(0f, max_timeStraight - 1f);
            }
        }
        if (turn)
        {
            turn = false;
            if (hitRight.collider == null && hitLeft.collider != null)
            {
                //straight = false;
                Debug.Log("turned right");
                transform.Rotate(new Vector3(0f, 0f, -90f));
                straight = true;
                if(timeToShoot == 0) {
                    moving = true;
                }
            }
            else if (hitLeft.collider == null && hitRight.collider != null)
            {
                //straight = false;
                Debug.Log("turned left");
                transform.Rotate(new Vector3(0f, 0f, 90f));
                straight = true;
                if(timeToShoot == 0) {
                    moving = true;
                }
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
                if(timeToShoot == 0) {
                    moving = true;
                }
            }
            else {
                //Cautionary measure in case an enemy gets stuck trying to turn between 2 walls
               if (!cameraTurnCause) {
                    timeStraight = max_timeStraight - Time.deltaTime;
                    straight = true;
                    if(timeToShoot == 0) {
                        moving = true;
                    }
                }
                //Sometimes will still turn around completely - mainly to deal with 1-way exits
                else {
                    transform.Rotate(new Vector3(0f, 0f, 180f));
                    straight = true;
                    if(timeToShoot == 0) {
                        moving = true;
                    }
                    timeStraight = Random.Range(max_timeStraight * 0.5f, max_timeStraight * 3f/4f);
                }
            }
            cameraTurnCause = false;
        }

        //if straight too long change directions
        //At the moment, commented out the modeswitcher, because the turn code at the bottom kinda replaced it
        //Biggest problem: Because of the way modeswitcher works, you can just toggle back and forth, not really changing anything
        /*if (timeStraight >= max_timeStraight)
        {
            straight = false;
            timeStraight = Random.Range(0f, max_timeStraight * 3f/4f);
            Debug.Log("not straight no more");
            // if the time going straight is too long roll to see if it stops, turns, or shoots
            modeSwitcher = Random.Range(0f, 1f);//1/3 - Turns; 2/3 - Shoots, 3/3 - Shoots and turns
            Debug.Log(modeSwitcher);
        }

        if (modeSwitcher <= 0.33f)
        {
            //waiting mode
            stopped = true;
            moving = false;
            shooting = false;
            modeSwitcher = 0;
        }
        else if (modeSwitcher <= 0.66f)
        {
            //moving mode
            moving = true;
            stopped = false;
            shooting = false;
            modeSwitcher = 0;
        }
        else if (modeSwitcher <= 1f)
        {
            //shooting mode
            shooting = true;
            stopped = false;
            moving = false;
            modeSwitcher = 0;//Hopefully this fixes a bug
        }
        */
        if(timeToShoot > 0) {
            timeToShoot -= Time.deltaTime;
            if(timeToShoot <= 0) {
                timeToShoot = 0;
                shooting = true;
            } 
        }
        //shooting mode
        if (shooting && timeShooting <= max_timeShooting)
        {
            //spawn a rock, increment counter
            //timeShooting += Time.deltaTime;
            if (rockShot < 1 && currentRock == null)
            {
                
                currentRock = Instantiate(rock, transform.position, transform.rotation);
                
                    rock.transform.eulerAngles = transform.up;
                
                rockShot=1;
            }
            //Added this in as a temporary measure
            stopped = true;
            timeStopped = 0;
            shooting = false;
            straight = true;
            timeStraight = Random.Range(0f, max_timeStraight * 3f/4f);;
            //timeShooting = 0;
            //rockShot = 0;
            //timeShooting++;
        }
        else if (shooting && timeShooting > max_timeShooting)
        {
            //go back to moving after shooting
            shooting = false;
            stopped = false;
            //timeStopped = Random.Range(0f, max_timeStopped * 2/3f);
            moving = true;
            straight = true;
            timeStraight = Random.Range(0f, max_timeStraight * 3f/4f);
            timeShooting = 0;
            //rockShot = 0;
        }

        //stopped modee
        if (stopped && timeStopped <= max_timeStopped)
        {
            timeStopped+=Time.deltaTime;
        }
        else if (stopped && timeStopped > max_timeStopped)
        {
            shooting = false;
            stopped = false;
            moving = true;
            straight = true;
            timeStraight = Random.Range(0f, max_timeStraight * 3f/4f);;
            //timeStopped = Random.Range(0f, max_timeStopped * 2/3f);
        }
        if(currentRock == null && rockShot == 1) {
            rockShot = 0;
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
    void OnDestroy() {
        if(myHP.health > 0 && currentRock != null) {
            Destroy(currentRock);
        }
    }
    //Used whenever the enemies "lands" on a new square, telling it whether it needs to turn
    void checkTurn(Vector3 direction, Vector3 pos) {
        //Needs to check to see if it wants to turn
        float rnd = Random.Range(0f, 1f);
        if(rnd < 0.23f) {
            turn = true;
            transform.position = pos;
            
        }

        //Needs to check to see if it wants to shoot
        else if(rnd < 0.3f) {
            timeToShoot = maxTimeToShoot;
            moving = false;
            //stopped = true;
            transform.position = pos;
        }
        else if(rnd < 0.34f) {
            timeToShoot = maxTimeToShoot;
            turn = true;
            moving = false;
            //stopped = true;
            transform.position = pos;
        }
    }
}
