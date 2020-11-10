using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PURPOSE: give an enemy object health
//USAGE: put this on an enemy object
public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;
    //public HeartSystem heartSystem;
    //public int damageDealt = -1;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

   public void TakeDamage(int damage){
       currentHealth -= damage;
       if(currentHealth <= 0){
           Die();
       }
   }

   void Die(){
       //Disable the enemy (player can walk pass it)
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
   }

    
    void OnTriggerEnter2D(Collider2D other){
        if ( other.tag == "Player" ) {
            other.gameObject.GetComponent<HeartSystem>().TakenDamage(-1);
        }
   }
    
}
