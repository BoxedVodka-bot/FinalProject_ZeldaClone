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
    public RoomManager myManager;
    public float spawnTime;//Amount of time for this enemy to spawn (not yet coded in)
    public bool takeKnockback;//A bool about whether this enemy will be knocked back when hit
    float takingKnockback;//Whether enemy is taking knockback
    Vector3 knockbackDir;//Direction of knocback
    public float knockbackForce;
    public bool wallCollision;//Whether enemy collides with walls
    void Start()
    {
        prev_health = health;
    }

    // Update is called once per frame
    void Update()
    {   
        if(invincibleTime > 0) {
            invincibleTime-= Time.deltaTime;
            if(invincibleTime <= 0) {
                invince = false;
                invincibleTime = 0;
            }
        }
        if(takingKnockback > 0) {
            takingKnockback -= Time.deltaTime;
            if(wallCollision) {
                //Checks to see if there's a wall before colliding - if there is, it stops
            }
            else {
                //will need to check to see if this pushes it off screen
                transform.position += knockbackForce * knockbackDir * Time.deltaTime;
            }
        }
    }
    //Also, the enemies deal damage directly to the player if they touch them
    void OnColliderEnter2D(Collider2D activator) {
        if(activator.CompareTag("Player")) {
            //If player not invincible (need to add this)
            activator.gameObject.GetComponent<HeartSystem>().TakenDamage(-1);
        }
    }

    //Whenever the enemy takes damage, they may become invincible for a moment
    public void TakeDamage(int damage, bool knockback, Vector3 knockbackDirection) {
        if(!invince) {
            health += damage;
            if(health <= 0) {
                //Destroy this enemy, and maybe drop an item
                Instantiate(deathAnimationPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));
                float rnd = Random.Range(0f, 1f);//Create a random number
                float percent = 0;//Set the current percentage to 0
                for(int i = 0; i < pickupPercent.Count; i++) {
                    percent += pickupPercent[i];//Add the percentage of the current pickup to the percent
                    if(percent >= rnd) {//if the percent is greater than the random number, you create a copy of that pickup
                        Transform myPickup = Instantiate(pickupList[i], transform.position, Quaternion.Euler(0f, 0f, 0f));
                        //Pickup is added to manager, so that when you leave the room, the pickup is destroyed
                        myManager.CurrentPickupList.Add(myPickup);
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
                if(knockback && takeKnockback) {
                    takingKnockback = 1f;
                    knockbackDir = knockbackDirection;
                }
            }
        }
    }
}
