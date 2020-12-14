using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: Used to destroy the death animation sprite that spawns when an enemy dies
//USAGE: attached to the death animation prefab - which has an animator
public class DeathAnimation : MonoBehaviour
{
    public float deathTime;
    public AudioSource myAudio;

    void Start() {
        myAudio.GetComponent<AudioSource>();
        myAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        deathTime -= Time.deltaTime;
        if(deathTime <= 0) {
           // Destroy(this.gameObject);
        }
    }
    void Death() {
        Destroy(this.gameObject);
    }
}
