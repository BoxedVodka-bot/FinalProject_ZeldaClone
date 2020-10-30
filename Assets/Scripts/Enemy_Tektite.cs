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
    float counter; //The tektites determine whether they actually do anything ever certain amount of time
    public float max_counter;
    void Start()
    {
        //Random Spawn
        //Spawns on a random tile that is a basic floor
    }

    // Update is called once per frame
    void Update()
    {   
        if(!jumping) {
            counter++;//counter rate is that of the idle animation
        }
        if(counter >= max_counter) {
            //Prep for the possibility of a new action
            counter = 0;
            //Possibility of prepping to jump in a random direction - can jump on walls
                //has a very small chance of jumping twice

            //Other possibility: prep, and then do nothing - high probability, but the chance to do something happens very often
        }
    }
}
