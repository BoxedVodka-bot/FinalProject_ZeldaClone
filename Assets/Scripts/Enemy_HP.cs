using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//PURPOSE: a script for all enemies, so that when they reach 0 health, they die
//USAGE: attached to all enemy objects
public class Enemy_HP : MonoBehaviour
{
    public int health;//Enemy health
    int prev_health;
    public bool invince;//Invincibility
    public float invincibleTime; //How long an enemy has been invincible for
    public float maxInvincibleTime;
    public Transform deathAnimationPrefab;
    public List<Transform> pickupList;
    public List<float> pickupPercent;
    //There are several variables which the enemy might just need for personal reasons, which are referenced here:
    public Camera myCamera;
    public Transform myPlayer;
    public Tilemap myTilemap;
    void Start()
    {
        prev_health = health;
    }

    // Update is called once per frame
    void Update()
    {   
        //Whenever the enemy takes damage, they may become invincible for a moment
        if(health != prev_health) {
            Debug.Log("j");
            prev_health = health;
            if(health <= 0) {
                //Destroy this enemy, and maybe drop an item
                Instantiate(deathAnimationPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));
                float rnd = Random.Range(0f, 1f);//Create a random number
                float percent = 0;//Set the current percentage to 0
                for(int i = 0; i < pickupPercent.Count; i++) {
                    percent += pickupPercent[i];//Add the percentage of the current pickup to the percent
                    if(percent >= rnd) {//if the percent is greater than the random number, you create a copy of that pickup
                        Instantiate(pickupList[i], transform.position, Quaternion.Euler(0f, 0f, 0f));
                        //Then break
                        break;
                    }
                }
                Destroy(this.gameObject);
            }
            else {
                //Maybe give knockback?
                //The enemy becomes invincible when they lose health but don't die, even if the invince is only for a second
                invince = true;
                invincibleTime = maxInvincibleTime;
            }
        }
        if(invincibleTime > 0) {
            invincibleTime-= Time.deltaTime;
            if(invincibleTime <= 0) {
                invince = false;
                invincibleTime = 0;
            }
        }
    }
}
