using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PseudoOctorok : MonoBehaviour
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
    float timeMoving;
    public float maxTimeMoving;
    float timeStopped;
    public float maxTimeStopped;
    public bool moving;
    public Ray2D frontDetect;
    public Ray2D leftDetect;
    public Ray2D rightDetect;
    public RaycastHit2D hitLeft;
    public RaycastHit2D hitRight;
    public RaycastHit2D hitFront;
    public Rigidbody2D octorokRB;
    public GameObject bulletPrefab;
    public Camera myCamera;
    public float statBarOffest;
    GameObject bullet;
    public float rnd;

    // Start is called before the first frame update
    void Start()
    {
        myHP = GetComponent<Enemy_HP>();
        myCamera = myHP.myCamera;
        counter = 0;
        timeStraight = 0;
        straight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)//If the enemy is moving straight, it keeps moving straight, and its straight time increases
        {
            transform.position +=(transform.up * speed * Time.deltaTime);
            timeStraight+=Time.deltaTime;//Not currently used
            timeMoving += Time.deltaTime;
            if(timeMoving >= maxTimeMoving) {
                moving = false;
                timeStopped = Random.Range(0f, maxTimeStopped * 2f/3f);
            }
        }
        else {
            timeStopped+= Time.deltaTime;
            if(timeStopped >= maxTimeStopped) {
                moving = true;
                timeMoving = Random.Range(0f, maxTimeMoving * 2f/3f);
            }
        }
        hitRight = Physics2D.Raycast(transform.position, transform.right * 1.5f, 1f);
        Debug.DrawRay(transform.position, transform.right * 1.5f, Color.red);
        hitLeft = Physics2D.Raycast(transform.position, -transform.right * 1.5f, 1f);
        Debug.DrawRay(transform.position, -transform.right * 1.5f, Color.blue);
        hitFront = Physics2D.Raycast(transform.position, transform.up * 1.5f, 1f);
        Debug.DrawRay(transform.position, transform.up * 1.5f, Color.green);
        LayerMask playerLayer = LayerMask.GetMask("Player");
        RaycastHit2D playerRight = Physics2D.Raycast(transform.position, Vector2.right, 100f, playerLayer);
        RaycastHit2D playerLeft = Physics2D.Raycast(transform.position, Vector2.left, 100f, playerLayer);
        RaycastHit2D playerUp = Physics2D.Raycast(transform.position, Vector2.up, 100f, playerLayer);
        RaycastHit2D playerDown = Physics2D.Raycast(transform.position, Vector2.down, 100f, playerLayer);
        if(playerRight.collider != null) {
            if(transform.up != Vector3.right) {
                //Has a chance to turn towards the player
                float random = Random.Range(0f, 1f);
                if(random <= 0.03f) {
                    transform.up = Vector3.right;
                }

            }
            else {
                float random = Random.Range(0f, 1f);
                if(random < 0.1f && bullet == null) {
                    bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                }
            }
        }
        else if(playerLeft.collider != null) {
            if(transform.up != -Vector3.right) {
                //Has a chance to turn towards the player
                float random = Random.Range(0f, 1f);
                if(random <= 0.03f) {
                    transform.up = -Vector3.right;
                }

            }
            else {
                float random = Random.Range(0f, 1f);
                if(random < 0.1f && bullet == null) {
                    bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                }
            }
        }
        else if(playerUp.collider != null) {
            if(transform.up != Vector3.up) {
                //Has a chance to turn towards the player
                float random = Random.Range(0f, 1f);
                if(random <= 0.03f) {
                    transform.up = Vector3.up;
                }

            }
            else {
                float random = Random.Range(0f, 1f);
                if(random < 0.1f && bullet == null) {
                    bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                }
            }
        }
        else if(playerDown.collider != null) {
            if(transform.up != Vector3.down) {
                //Has a chance to turn towards the player
                float random = Random.Range(0f, 1f);
                if(random <= 0.03f) {
                    transform.up = Vector3.down;
                }

            }
            else {
                float random = Random.Range(0f, 1f);
                if(random < 0.1f && bullet == null) {
                    bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                }
            }
        }
        rnd = Random.Range(0f, 1f);
        if(rnd < 0.01f || hitFront.collider != null ) {
            Turn();
        }
        Vector3 checkAgainstCamera = transform.position + (speed * 5f * Time.deltaTime) * transform.up;
        if(checkAgainstCamera.x >= myCamera.transform.position.x - myCamera.orthographicSize * myCamera.aspect + 1.5f) {
            
            //Turn();
        }
        else if(myCamera.transform.position.y + myCamera.orthographicSize - 3.5f < checkAgainstCamera.y ) {
                Turn();
        }
        else if(myCamera.transform.position.y - myCamera.orthographicSize + 1.5f > checkAgainstCamera.y ) {
            Turn();
        }
        else if(myCamera.transform.position.x + myCamera.orthographicSize * myCamera.aspect - 1.5f < checkAgainstCamera.x ) {
            Turn();
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
    void Turn() {
            if (hitRight.collider == null && hitLeft.collider != null)
            {
                straight = false;
                Debug.Log("turned right");
                transform.eulerAngles += new Vector3(0f, 0f, -90f);
            }
            else if (hitLeft.collider == null && hitRight.collider != null)
            {
                straight = false;
                Debug.Log("turned left");
                transform.eulerAngles += new Vector3(0f, 0f, 90f);
            }
            else if (hitLeft.collider == null && hitLeft.collider == null)
            {
                rnd = Random.Range(0f, 1f);
                if (rnd >= .5f)
                {
                    Debug.Log("turned right");
                    transform.eulerAngles += new Vector3(0f, 0f, -90f);

                }
                if (rnd < .5f)
                {
                    Debug.Log("turned left");
                    transform.eulerAngles += new Vector3(0f, 0f, 90f);
                }
            }
            else {
                transform.eulerAngles += new Vector3(0f, 0f, 180f);
            }
    }
}
