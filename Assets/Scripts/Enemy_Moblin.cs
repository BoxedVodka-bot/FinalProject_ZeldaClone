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
    GameObject currentSpear;
    Camera myCamera;
    public Rigidbody2D moblinRB;
    public Animator myAnimator; //Currently commenting this out, until we have an actual working animator
    public bool cameraTurnCause;

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
    public float timeToShoot;
    public float max_timeToShoot;
    public int spearsThrown;
    Animator anim;
    void Start()
    {
        myHP = GetComponent<Enemy_HP>();
        myCamera = myHP.myCamera;
        moblinRB = GetComponent<Rigidbody2D>();
        //moveSwitcher = Random.Range(0, 4);
        moveSwitcher = 0;
        moveSwitcher = 0;
        modeSwitcher = 1;
        moving = true;
        spearsThrown = 0;
        anim = GetComponent<Animator>();

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
        

        if (hitFront.collider == null)
        {
            if (moving && right)
            {
                if (transform.position.x > myCamera.transform.position.x + myCamera.orthographicSize * myCamera.aspect - 1f)
                {
                    cameraTurnCause = true;
                    collisionCheck();
                }
                else if (timeRight < max_timeRight)
                {
                    transform.position += transform.right * speed * Time.deltaTime;
                    timeRight += Time.deltaTime;
                }
                else {
                    collisionCheck();
                }
            }

            if (moving && left)
            { 
                if (transform.position.x < myCamera.transform.position.x - myCamera.orthographicSize * myCamera.aspect + 1f)
                {
                    cameraTurnCause = true;
                    collisionCheck();
                }
                else if (timeLeft < max_timeLeft)
                {
                    transform.position += -transform.right * speed * Time.deltaTime;
                    timeLeft += Time.deltaTime;
                }
                else {
                    collisionCheck();
                }
            }

            if (moving && down)
            {
                if (transform.position.y < myCamera.transform.position.y - myCamera.orthographicSize + 1f)
                {
                    cameraTurnCause = true;
                    collisionCheck();
                }
                else if (timeDown < max_timeDown)
                {
                    transform.position += -transform.up * speed * Time.deltaTime;
                    timeDown += Time.deltaTime;
                }
                else {
                    collisionCheck();
                }
            }

            if (moving && up)
            {
                if (transform.position.y > myCamera.transform.position.y + myCamera.orthographicSize - 3f)
                {
                    cameraTurnCause = true;
                    collisionCheck();
                }
                else if (timeUp < max_timeUp)
                {
                    transform.position += transform.up * speed * Time.deltaTime;
                    timeUp += Time.deltaTime;
                }
                else {
                    collisionCheck();
                }
            }
        }
        else if (hitFront.collider != null)
        {
            collisionCheck();
        }

        timeToShoot-= Time.deltaTime;
        if (timeToShoot <= 0)
        {
            shoot();
        }

        //Commenting this out until we have a working animation
        //myAnimator.SetInteger("moveSwitcher", moveSwitcher);

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
            anim.SetInteger("Walk_X", -1);
            anim.SetInteger("Walk_Y", 0);
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
           // Debug.Log("moving left");
        }
        // moving right state
        else if (moving && moveSwitcher == 2)
        {
            moving = true;
            left = false;
            right = true;
            down = false;
            up = false;
            anim.SetInteger("Walk_X", 1);
            anim.SetInteger("Walk_Y", 0);
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
            //Debug.Log("moving right");
        }
        // moving down state
        else if (moving && moveSwitcher == 3)
        {
            moving = true;
            left = false;
            right = false;
            down = true;
            up = false;
            anim.SetInteger("Walk_X", 0);
            anim.SetInteger("Walk_Y", -1);
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
            //Debug.Log("moving down");
        }
        // moving up state
        else if (moving && moveSwitcher == 4)
        {
            moving = true;
            left = false;
            right = false;
            down = false;
            up = true;
            anim.SetInteger("Walk_X", 0);
            anim.SetInteger("Walk_Y", 1);
            shooting = false;
            //timeUp++;
            timeLeft = 0;
            timeRight = 0;
            timeDown = 0;
            //orients the raycasts in the appropriate direction
            myFront = transform.up;
            myRight = transform.right;
            myLeft = -transform.right;
            //Debug.Log("moving up");
        }
    }
    public void collisionCheck()
    {
        
            timeRight = Random.Range(0, Mathf.RoundToInt(max_timeRight * 3/4));
            timeLeft = Random.Range(0, Mathf.RoundToInt(max_timeLeft * 3/4));
            timeDown = Random.Range(0, Mathf.RoundToInt(max_timeDown * 3/4));
            timeUp = Random.Range(0, Mathf.RoundToInt(max_timeUp * 3/4));
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
        else if (cameraTurnCause)
        {
            turnDir(2);
        }
        else {
        }
        cameraTurnCause = false;
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
                moveSwitcher = 4;
            }
        }
        // if we are turning around because of the camera bounds, dir = 2
        if (dir == 2)
        {
            Debug.Log("turn around");
            if (myFront == transform.up)
            {
                // if going up and turning around, go down
                moveSwitcher = 3;
            }
            else if (myFront == transform.right)
            {
                // if going right and turning around, go left
                moveSwitcher = 1;
            }
            else if (myFront == -transform.up)
            {
                // if going down and turning around, go up
                moveSwitcher = 4;
            }
            else if (myFront == -transform.right)
            {
                // if going left and turning around, go right
                moveSwitcher = 2;
            }
        }
    }

    public void shoot()
    {
        if(currentSpear == null) {
        if (myFront == transform.up)
        {
            currentSpear = Instantiate(spear, transform.position, Quaternion.identity);
        }
        else if (myFront == transform.right)
        {
            currentSpear = Instantiate(spear, transform.position, Quaternion.Euler(new Vector3(0f, 0f, -90f)));
        }
        else if (myFront == -transform.up)
        {
            currentSpear = Instantiate(spear, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 180f)));
        }
        else if (myFront == -transform.right)
        {
            currentSpear = Instantiate(spear, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 90f)));
        }
        }
        timeToShoot = Random.Range(max_timeToShoot * 2f / 3f, max_timeToShoot);
    }
    void OnDestroy() {
        if(currentSpear != null && myHP.health > 0) {
            Destroy(currentSpear);
        }
    }
    
}
