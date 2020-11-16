using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: allows the Peahat to fly around and avoid attacks
//USAGE: Attached to a Peahat (moth-like creature) Prefab
public class Enemy_Peahat : MonoBehaviour
{
    public bool flying;
    public float baseSpeed;
    float speed;
    //int peahatHealth = 2;
    float flyTime;//How long the Peahat has been flying for
    float sitTime;
    public float max_sitTime;
    public float max_flyTime;
    bool slowDown;
    bool speedUp;
    float timeStraight;
    public float max_timeStraight;
    Enemy_HP myHP;
    public Camera myCamera;
    public float statBarOffset;
    SpriteRenderer mySpriteRenderer;
    Vector3 flyDirection;//The direction this Peahat is currently flying in (determined semi-randomly)

    void Start()
    {
        speed = baseSpeed;
        flying = true;
        //Direction is determined to be a random direction
        float x = Random.Range(-1, 1);
        float y = Random.Range(-1, 1);
        //If direction is null, it is determined differently
        if(x == 0 && y == 0) {
            float rnd =Random.Range(0, 1f);
            if(rnd <0.25f) {
                y = 1f;
            }
            else if(rnd < 0.5f) {
                y = -1f;
            }
            else if(rnd <0.75f ){
                x = 1f;
            }
            else {
                x = -1f;
            }
        }
        flyDirection = new Vector3(x, y, 0f).normalized;
        myHP = GetComponent<Enemy_HP>();
        myCamera = myHP.myCamera;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //Determines a random spot to spawn in
        x = Random.Range(myCamera.transform.position.x - myCamera.aspect *myCamera.orthographicSize + 1f, myCamera.transform.position.x + myCamera.orthographicSize *myCamera.aspect - 1f);
        y = Random.Range(myCamera.transform.position.y - myCamera.orthographicSize + 1f, myCamera.transform.position.y + myCamera.orthographicSize - 1f - statBarOffset);
        Debug.Log(myCamera.transform.position.ToString());
        //Debug.Log(x.ToString());
        //Debug.Log(y.ToString());
        transform.position = new Vector3(x, y, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        //flies around semi-randomly - can fly over walls
        if(flying) {
            myHP.invince = true;
        timeStraight-=Time.deltaTime;
        float rnd = Random.Range(0f, 1f);
        if(timeStraight <= 0 ) {

            //EDIT QUESTION: Maybe, should only be able to shift direction by 45 degrees (1/8th turn) at a time? - Could probably mean it could turn more often
            if(rnd < 0.06f) {
                timeStraight = Random.Range(0.5f, max_timeStraight);
                //Turn 45 degrees right
                if(flyDirection.x > 0) {
                    if(flyDirection.y > 0) {
                        flyDirection = new Vector3(0f, 1f, 0f);
                    }
                    else if(flyDirection.y < 0) {
                        flyDirection = new Vector3(1, 0f, 0f);
                    }
                    else {
                        flyDirection = new Vector3(1f, 1f, 0f).normalized;
                    }
                }
                else if(flyDirection.x < 0) {
                    if(flyDirection.y > 0) {
                        flyDirection = new Vector3(-1f, 0f, 0f);
                    }
                    else if(flyDirection.y < 0) {
                        flyDirection = new Vector3(0f, -1f, 0f);
                    }
                    else {
                        flyDirection = new Vector3(-1f, -1f, 0f).normalized;
                    }
                }
                else if(flyDirection.y > 0) {
                    flyDirection = new Vector3(-1f, 1f, 0f).normalized;
                }
                else if(flyDirection.y < 0) {
                    flyDirection = new Vector3(1f, -1f, 0f).normalized;
                }
            }
            else if(rnd < 0.12f) {
                timeStraight = Random.Range(0.5f, max_timeStraight);
                //Turn 45 degrees left
                if(flyDirection.x > 0) {
                    if(flyDirection.y > 0) {
                        flyDirection = new Vector3(1f, 0f, 0f);
                    }
                    else if(flyDirection.y < 0) {
                        flyDirection = new Vector3(0, -1f, 0f);
                    }
                    else {
                        flyDirection = new Vector3(1f, -1f, 0f).normalized;
                    }
                }
                else if(flyDirection.x < 0) {
                    if(flyDirection.y > 0) {
                        flyDirection = new Vector3(0f, 1f, 0f);
                    }
                    else if(flyDirection.y < 0) {
                        flyDirection = new Vector3(-1f, 0f, 0f);
                    }
                    else {
                        flyDirection = new Vector3(-1f, 1f, 0f).normalized;
                    }
                }
                else if(flyDirection.y > 0) {
                    flyDirection = new Vector3(1f, 1f, 0f).normalized;
                }
                else if(flyDirection.y < 0) {
                    flyDirection = new Vector3(-1f, -1f, 0f).normalized;
                }
            }
            
            else {
                //timeStraight-=0.5f;
            }
        }
        //If the peahat's position + their direction * speed would be off screen, they turn around
        Vector3 checkAgainstCamera = transform.position + flyDirection *speed * Time.deltaTime;
        bool reverse = false;
        if(flyDirection.x > 0) {
            //If the x position of the camera + the camera width / 2 - 1 is less than your position, reverse
            if(myCamera.transform.position.x + myCamera.orthographicSize * myCamera.aspect - 1.5f < checkAgainstCamera.x ) {
                reverse = true;
            }
        }
        else if(flyDirection.x < 0) {
            if(myCamera.transform.position.x - myCamera.orthographicSize * myCamera.aspect + 1.5f > checkAgainstCamera.x ) {
                reverse = true;
            }
        }
        if(flyDirection.y > 0) {
            //If the x position of the camera + the camera width / 2 - 1 is less than your position, reverse
            if(myCamera.transform.position.y + myCamera.orthographicSize - 3.5f < checkAgainstCamera.y ) {
                reverse = true;
            }
        }
        else if(flyDirection.y < 0) {
            if(myCamera.transform.position.y - myCamera.orthographicSize + 1.5f > checkAgainstCamera.y ) {
                reverse = true;
            }
        }
        if(reverse) {
            flyDirection = -flyDirection;
        }

        transform.position += flyDirection * speed *Time.deltaTime;
        flyTime+=Time.deltaTime;
        if(flyTime >= max_flyTime) {
            slowDown = true;
        }
        }
        else {
            //if sitting, it reacharges, until it can start flying again
            sitTime += Time.deltaTime;
            if(sitTime >= max_sitTime) {
                sitTime = Random.Range(0f, max_sitTime / 4);
                speedUp = true;
                flying = true;
            }
        }
        //after a period of time (which will be semi-random), gets tired and "sits down", at which point it can be attacked
        if(speedUp) {
            speed += Time.deltaTime;
            if(speed >= baseSpeed) {
                speed = baseSpeed;
                speedUp = false;
            }
        }
        if(slowDown) {
            speed -= Time.deltaTime;
            if(speed <=0) {
                speed = 0;
                flyTime = Random.Range(0f, max_flyTime / 2);
                slowDown = false;
                flying = false;
                myHP.invince = false;
            }
        }
        //Using this color renderer as a stand-in for sprite aniamtion speed
        mySpriteRenderer.color = new Color(speed / 2f, speed / 2f, 0f, 1f);
    }
}
