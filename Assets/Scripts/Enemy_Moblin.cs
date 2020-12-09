using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: move moblin enemy type and manage its behaviors (shoot, walk, turn, etc.)
//USAGE: attached to moblin prefab

public class Enemy_Moblin : MonoBehaviour
{
    [Header("Modes")]
    public bool moving;
    public bool right;
    public bool left;
    public bool up;
    public bool down;
    public bool stopped;
    public bool shooting;
    public int modeSwitcher;
    public int moveSwitcher;

    [Header("Raycasting stuff")]
    public RaycastHit2D hitLeft;
    public RaycastHit2D hitRight;
    public RaycastHit2D hitFront;
    public float rayDist;
    public Vector3 myRight;
    public Vector3 myFront;
    public Vector3 myLeft;

    [Header("Attributes")]
    Enemy_HP myHP;
    public float speed;
    public bool strongVersion;
    public GameObject spear;
    Camera myCamera;
    public Rigidbody2D moblinRB;

    [Header("Timers and Counters")]
    public float timeLeft;
    public float max_timeLeft;
    public float timeRight;
    public float max_timeRight;
    public float timeUp;
    public float max_timeUp;
    public float timeDown;
    public float max_timeDown;
    public float timeStopped;
    public float max_timeStopped;
    public int spearsThrown;

    void Start()
    {
        myHP = GetComponent<Enemy_HP>();
        myCamera = myHP.myCamera;
        moblinRB = GetComponent<Rigidbody2D>();
        //moveSwitcher = Random.Range(0, 4);
        moveSwitcher = 0;
        moveSwitcher = 0;
        modeSwitcher = 0;
        moving = true;
        spearsThrown = 0;

    }

    
    void Update()
    {
        // set the layer it is detecting in
        LayerMask mask = LayerMask.GetMask("Wall");
        // right ray
        hitRight = Physics2D.Raycast(transform.position, myRight, rayDist * 2f, mask);
        Debug.DrawRay(transform.position, myRight * rayDist * 2f, Color.red);
        // left ray
        hitLeft = Physics2D.Raycast(transform.position, myLeft, rayDist * 2f, mask);
        Debug.DrawRay(transform.position, myLeft * rayDist * 2f, Color.blue);
        // front ray
        hitFront = Physics2D.Raycast(transform.position, myFront, rayDist, mask);
        Debug.DrawRay(transform.position, myFront * rayDist, Color.green);

        // pick a direction if in the moving state and no direction has been assigned
        if (moving && moveSwitcher == 0)
        {
            moveSwitcher = Random.Range(1, 5);
        }
        // moving left state
        if (moving && moveSwitcher == 1)
        {
            moving = true;
            left = true;
            right = false;
            down = false;
            up = false;
            shooting = false;
            stopped = false;
            //timeLeft++;
            timeRight = 0;
            timeDown = 0;
            timeUp = 0;
            //orients the raycasts in the appropriate direction
            myFront = -transform.right;
            myRight = transform.up;
            myLeft = -transform.up;
            Debug.Log("moving left");
        }
        // moving right state
        else if (moving && moveSwitcher == 2)
        {
            moving = true;
            left = false;
            right = true;
            down = false;
            up = false;
            shooting = false;
            stopped = false;
            //timeRight++;
            timeLeft = 0;
            timeDown = 0;
            timeUp = 0;
            //orients the raycasts in the appropriate direction
            myFront = transform.right;
            myRight = -transform.up;
            myLeft = transform.up;
            Debug.Log("moving right");
        }
        // moving down state
        else if (moving && moveSwitcher == 3)
        {
            moving = true;
            left = false;
            right = false;
            down = true;
            up = false;
            shooting = false;
            stopped = false;
            //timeDown++;
            timeLeft = 0;
            timeRight = 0;
            timeUp = 0;
            //orients the raycasts in the appropriate direction
            myFront = -transform.up;
            myRight = -transform.right;
            myLeft = transform.right;
            Debug.Log("moving down");
        }
        // moving up state
        else if (moving && moveSwitcher == 4)
        {
            moving = true;
            left = false;
            right = false;
            down = false;
            up = true;
            shooting = false;
            //timeUp++;
            timeLeft = 0;
            timeRight = 0;
            timeDown = 0;
            //orients the raycasts in the appropriate direction
            myFront = transform.up;
            myRight = transform.right;
            myLeft = -transform.right;
            Debug.Log("moving up");
        }

        if (hitFront.collider == null)
        {
            if (moving && right)
            {
                if (timeRight < max_timeRight)
                {
                    transform.position += transform.right * speed * Time.deltaTime;
                    timeRight += Time.deltaTime;
                }
                else
                    collisionCheck();
            }

            if (moving && left)
            {
                if (timeLeft < max_timeLeft)
                {
                    transform.position += -transform.right * speed * Time.deltaTime;
                    timeLeft += Time.deltaTime;
                }
                else
                    collisionCheck();
            }

            if (moving && down)
            {
                if (timeDown < max_timeDown)
                {
                    transform.position += -transform.up * speed * Time.deltaTime;
                    timeDown += Time.deltaTime;
                }
                else
                    collisionCheck();
            }

            if (moving && up)
            {
                if (timeUp < max_timeUp)
                {
                    transform.position += transform.up * speed * Time.deltaTime;
                    timeUp += Time.deltaTime;
                }
                else
                    collisionCheck();
            }
        }
        else if (hitFront.collider != null)
        {
            collisionCheck();
        }

    }
    public void collisionCheck()
    {
        Debug.Log("it hit something");
        if (hitLeft.collider == null && hitRight.collider != null)
        {
            turnDir(0);

        }
        else if (hitRight.collider == null && hitLeft.collider != null)
        {
            turnDir(1);
        }
        else if (hitLeft.collider == null && hitRight.collider == null)
        {
            Debug.Log("picked randomly");
            turnDir(Random.Range(0, 2));
        }
    }
    public void turnDir(int dir)
    {
        // if we are turning left, dir = 0
        if (dir == 0)
        {
            Debug.Log("turn left");
            if (myFront == transform.up)
            {
                // if going up and turning left, go left
                moveSwitcher = 1;
            }
            else if (myFront == -transform.right)
            {
                // if going left and turning left, go down
                moveSwitcher = 3;
            }
            else if (myFront == -transform.up)
            {
                // if going down and turning left, go right
                moveSwitcher = 2;
            }
            else if (myFront == transform.right)
            {
                // if going right and turning left, go up
                moveSwitcher = 4;
            }
        }
        // if we are turning right dir = 1
        if (dir == 1)
        {
            Debug.Log("turn right");
            if (myFront == transform.up)
            {
                // if going up and turning right, go right
                moveSwitcher = 2;
            }
            else if (myFront == transform.right)
            {
                // if going right and turning right, go down
                moveSwitcher = 3;
            }
            else if (myFront == -transform.up)
            {
                // if going down and turning right, go left
                moveSwitcher = 1;
            }
            else if (myFront == -transform.right)
            {
                // if going left and turning right, go up
                moveSwitcher = 2;
            }
        }
    }
}
