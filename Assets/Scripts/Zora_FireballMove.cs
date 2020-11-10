using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: Used to shoot the fireball at the player
//USAGE: Attached to the Zora's fireball prefab
public class Zora_FireballMove : MonoBehaviour
{
    public Transform myPlayer;
    public float speed;
    public float lifeSpan;
    Vector3 direction;
    
    void Start()
    {
        //Set the fireball's rotation to be pointed towards the player's current spot
        direction = Vector3.Normalize(myPlayer.position - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Fireball moves. If it has lived too long, it dies
        transform.position += direction * speed;
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0) {
            Destroy(this.gameObject);
        }
    }
}
