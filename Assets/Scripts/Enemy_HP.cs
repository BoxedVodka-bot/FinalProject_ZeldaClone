using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: a script for all enemies, so that when they reach 0 health, they die
//USAGE: attached to all enemy objects
public class Enemy_HP : MonoBehaviour
{
    public int health;//Enemy health
    int prev_health;
    public float invincibleTime; //How long an enemy has been invincible for
    public float maxInvincibleTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //Whenever the enemy takes damage, they may become invincible for a moment
        if(health != prev_health) {
            if(health == 0) {
                //Destroy this enemy, and maybe drop an item
            }
            else {
                //Maybe give knockback?
                invincibleTime = maxInvincibleTime;
            }
        }
        if(invincibleTime > 0) {
            invincibleTime-= Time.deltaTime;
            if(invincibleTime <= 0) {
                invincibleTime = 0;
            }
        }
    }
}
