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
    public bool moving;
    public Ray2D frontDetect;
    public Ray2D leftDetect;
    public Ray2D rightDetect;
    public RaycastHit2D hitLeft;
    public RaycastHit2D hitRight;
    public RaycastHit2D hitFront;
    public Rigidbody2D octorokRB;
    public float rnd;

    // Start is called before the first frame update
    void Start()
    {
        myHP = GetComponent<Enemy_HP>();
        counter = 0;
        timeStraight = 0;
        straight = true;
    }

    // Update is called once per frame
    void Update()
    {

        hitRight = Physics2D.Raycast(transform.position, Vector2.right * 1.5f, 1f);
        Debug.DrawRay(transform.position, Vector2.right * 1.5f, Color.red);
        hitLeft = Physics2D.Raycast(transform.position, Vector2.left * 1.5f, 1f);
        Debug.DrawRay(transform.position, Vector2.left * 1.5f, Color.blue);
        hitFront = Physics2D.Raycast(transform.position, Vector2.up * 1.5f, 1f);
        Debug.DrawRay(transform.position, Vector2.up * 1.5f, Color.green);

        if (straight)
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
            timeStraight++;

        }

        if (timeStraight >= max_timeStraight)
        {
            straight = false;
            timeStraight = 0;

            if (hitRight.collider == null && hitLeft.collider != null)
            {
                straight = false;
                Debug.Log("turned right");
            }
            if (hitLeft.collider == null && hitRight.collider != null)
            {
                straight = false;
                Debug.Log("turned left");
            }
            if (hitLeft.collider == null && hitLeft.collider == null)
            {
                rnd = Random.Range(0f, 1f);
                if (rnd >= .5f)
                {
                    Debug.Log("turned right");

                }
                if (rnd < .5f)
                {
                    Debug.Log("turned left");
                }
            }
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
