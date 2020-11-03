using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Code for the water enemies that shoot at the player
//USAGE: Attached to a Zora Prefab
public class Enemy_Zora : MonoBehaviour
{
    //This enemy's health
    int zoraHealth = 2;
    //The time this enemy spends above the surface of the water
    float surfaceTime = 0;
    public float max_surfaceTime;
    //The time this enemy spends below the surface of the water
    float diveTime = 0;
    public float max_diveTime;
    //Whether or not this enemy has surfaced
    public bool hasSurfaced = true;
    //A boolean used for when the Zora needs to move to a new random place
    bool positionReset;
    SpriteRenderer mySpriteRenderer;
    public Transform myPlayer;
    Enemy_HP myHP;
    //This enemy's projectile
    public Transform myFireballPrefab;
    //The delay to shoot the projectile after surfacing
    float shotDelay = 0;
    public float max_shotDelay;
    //Whether or not the enemy needs to shoot a projectile
    bool shoot = false;
    void Start()
    {
        myHP = GetComponent<Enemy_HP>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(hasSurfaced) {
            surfaceTime += Time.deltaTime;
            if(surfaceTime >= max_surfaceTime) {
                surfaceTime = 0;
                hasSurfaced = false;
                mySpriteRenderer.color = Color.blue;
                myHP.health = zoraHealth;
                positionReset = false;
            }
        }
        else {
            myHP.invince = true;
            diveTime += Time.deltaTime;
            if(diveTime > max_diveTime) {
                diveTime = 0;
                hasSurfaced = true;
                mySpriteRenderer.color = Color.white;
                shoot = true;
            }
        }
        if(shoot) {
            shotDelay +=Time.deltaTime;
            if(shotDelay >= max_shotDelay ) {
                shotDelay = 0;
                shoot = false;
                Transform fireball = Instantiate(myFireballPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));
                Zora_FireballMove myFire = fireball.GetComponent<Zora_FireballMove>();
                myFire.myPlayer = myPlayer;
            }
        }
        //Halfway through its respawn, the Zora's position is reset
        if(diveTime > max_diveTime / 2 && !positionReset) {
            positionReset = true;
            //Gonna probably use tilemapping for this
        }
    }
}
