    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: Movement code for the Tektites (jumping spiders)
//USAGE: attached to Tektite Prefab
public class Enemy_Tektite : MonoBehaviour
{
    public float speed;
    int tektiteHealth;
    public bool strongVersion;
    bool jumping;
    bool doubleJump;
    float jumpTime;
    public float max_jumpTime;
    float counter; //The tektites determine whether they actually do anything ever certain amount of time
    public float max_counter;
    Vector3 jumpDirection; // This is used to determine the place the tektite is jumping
    void Start()
    {
        //Random Spawn
        //Spawns on a random tile that is a basic floor
    }

    // Update is called once per frame
    void Update()
    {   
        if(!jumping) {
            counter+=Time.deltaTime;//counter rate is that of the idle animation
        }
        if(counter >= max_counter) {
            //Prep for the possibility of a new action
            counter = 0;
            float rnd = Random.Range(0f, 1f);
            //Possibility of prepping to jump in a random direction - can jump on walls
            if(rnd < 0.3f) {//This number is currently a placeholder
                jumpDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
                jumping = true;
                //has a very small chance of jumping twice
                if(rnd < 0.05f) {
                    doubleJump = true;
                }
                else {
                    doubleJump = false;
                }
            }

            //Other possibility: prep, and then do nothing - high probability, but the chance to do something happens very often
        }
        if(jumping) {
            
            //BUGGGGGG: Need to figure out how to get the jumping as an arc, rather than a weird two-line thing
            
            jumpTime += Time.deltaTime;
            if(jumpTime < max_jumpTime / 2) {
                if(jumpDirection.y > jumpDirection.x) {
                    transform.position += (jumpDirection + transform.up / 2).normalized *Time.deltaTime * speed;
                }
                else {
                    transform.position += jumpDirection *speed * Time.deltaTime;
                }
            }
            else if(jumpTime < max_jumpTime) {
                if(jumpDirection.y > jumpDirection.x) {
                    transform.position += (jumpDirection - transform.up / 2).normalized *Time.deltaTime * speed;
                }
                else {
                    transform.position += jumpDirection *speed * Time.deltaTime;
                }
            }
            else {
                jumpTime = 0;
                if(doubleJump) {
                    jumpDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
                    doubleJump = false;
                }
                else {
                    jumping = false;
                }
            }
        }
    }
}
