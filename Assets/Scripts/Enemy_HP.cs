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
            float statBarOffest = 2f;
            float edge = 1f;
            bool hasStopped = false;
            if(knockbackDir.x > 0) {
                if(transform.position.x + knockbackForce*Time.deltaTime > myCamera.transform.position.x + myCamera.aspect * myCamera.orthographicSize - edge) {
                    transform.position = new Vector3(myCamera.transform.position.x + myCamera.aspect * myCamera.orthographicSize - edge, transform.position.y, transform.position.z);
                    hasStopped = true;
                    takingKnockback = 0;
                }
            }
            else if(knockbackDir.x < 0) {
                if(transform.position.x - knockbackForce*Time.deltaTime < myCamera.transform.position.x - myCamera.aspect * myCamera.orthographicSize + edge) {
                    transform.position = new Vector3(myCamera.transform.position.x - myCamera.aspect * myCamera.orthographicSize + edge, transform.position.y, transform.position.z);
                    hasStopped = true;
                    takingKnockback = 0;
                }
            }
            else if(knockbackDir.y > 0) {
                if(transform.position.y + knockbackForce*Time.deltaTime > myCamera.transform.position.y + myCamera.orthographicSize - edge - statBarOffest) {
                    transform.position = new Vector3(transform.position.x, myCamera.transform.position.y + myCamera.orthographicSize - edge - statBarOffest, transform.position.z);
                    hasStopped = true;
                    takingKnockback = 0;
                }
            }
            else if(knockbackDir.y < 0) {
                if(transform.position.y - knockbackForce*Time.deltaTime < myCamera.transform.position.y - myCamera.orthographicSize + edge) {
                    transform.position = new Vector3(transform.position.x, myCamera.transform.position.y - myCamera.orthographicSize +edge, transform.position.z);
                    hasStopped = true;
                    takingKnockback = 0;
                }
            }
            if(wallCollision) {
                //Checks to see if there's a wall before colliding - if there is, it stops
                //Will need an overlap box or overlap circle
                //Not sure if this is working rn, but I gotta take a break
                LayerMask wall = LayerMask.GetMask("Wall");
                Collider2D wallCheck = Physics2D.OverlapCircle(transform.position + knockbackForce * knockbackDir * Time.deltaTime, transform.localScale.x / 2.4f, wall);
                if(wallCheck != null) {
                    hasStopped = true;
                    takingKnockback = 0;
                }
            }
            if(!hasStopped) {
                //will need to check to see if this pushes it off screen
                transform.position += knockbackForce * knockbackDir * Time.deltaTime;
            }
        }
    }
    //Also, the enemies deal damage directly to the player if they touch them
    //Coldier not working at ALL
    void OnTriggerEnter2D(Collider2D activator) {
            Debug.Log("HIT");
        if(activator.CompareTag("PlayerCollision")) {
            //If player not invincible (need to add this)
            //Should only bounce back in straight directions
            PlayerCollisionInfo P = activator.GetComponent<PlayerCollisionInfo>();
            PlayerControl pControl = P.myPlayerControl;//Force for the push, might be changed in the future
            //I migrated everything over to the player, so that other objects can also deal knockback
            pControl.EnemyCollision(transform.position, -1);
            //if(!pControl.invincibility) {
                //pControl.invincibility = true;//Needs to also pause the player
                //pControl.invincibilityTime = pControl.maxInvincibilityTime;
                //Rigidbody2D rb = P.myPlayer.GetComponent<Rigidbody2D>();
                //Vector3 vectorFromMonsterToPlayer = activator.transform.position - transform.position;
                //vectorFromMonsterToPlayer.Normalize();
                //Vector2 my2Dvector = new Vector2(vectorFromMonsterToPlayer.x, vectorFromMonsterToPlayer.y );
                //Vector2 myVector = CalculateVector(vectorFromMonsterToPlayer);
                //rb.velocity = myVector * pControl.force;
            //Force needs to be stopped in the future
                //P.myHeartSystem.TakenDamage(-1);
            //}
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
                        Destroy(this.gameObject);
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
                    takingKnockback = 0.5f;
                    knockbackDir = knockbackDirection;
                }
            }
        }
    }
    Vector2 CalculateVector(Vector3 distance) {
        Vector2 endCalc = new Vector2(0f, 0f);
        float x = distance.x;
        float y = distance.y;
        float x_abs = Mathf.Abs(x);
        float y_abs = Mathf.Abs(y);
        if(x_abs > y_abs) {
            endCalc = new Vector2(x, 0f).normalized;
        }
        else if(y_abs > x_abs) {
            endCalc = new Vector2(0f, y).normalized;
        }
        else {
            float rnd = Random.Range(0f, 1f);
            if(rnd < 0.5f) {
                endCalc = new Vector2(x, 0f).normalized;
            }
            else {
                endCalc = new Vector2(0f, y).normalized;
            }
        }
        return endCalc;
    }
}
