using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PUROSE: A collider class that checks when the player hits the fireball (or vice versa)
//USAGE: Attached to the fireball prefab
public class Zora_FireballCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D activator) {
        if(activator.CompareTag("Player")) {
            //The player takes damage unless their shield is up, and the fireball is destroyed
            if(activator.transform.eulerAngles.z == 90f) {

            }
            else {
                //Deals damage to the player (1/2 or 1?)
            }
            
            Destroy(this.gameObject);
        }
    }
}
