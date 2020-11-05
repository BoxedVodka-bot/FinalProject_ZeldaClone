using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Might not use this at all, just wanted to ahve it for posterity
public class Enemy_Leever : MonoBehaviour
{
    public float speed;
    public int leeverHealth;
    public bool strongVersion;//The blue version of this is the "strong version", with more health and more complex movement
    float surfaceTime = 0;
    public float max_surfaceTime;
    float underTime = 0;
    Vector3 direction;
    public float max_underTime;
    public bool hasSurfaced = false;
    // Start is called before the first frame update
    void Start()
    {
        if(strongVersion) {
            leeverHealth = 3;
        }
        else {
            leeverHealth = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasSurfaced) {
            surfaceTime+=Time.deltaTime;
            if(surfaceTime > max_surfaceTime) {
                surfaceTime = 0;
                hasSurfaced = false;
            }
        }
        else {
            underTime +=Time.deltaTime;
            if(underTime > max_underTime) {
                underTime = 0;
                hasSurfaced = true;
                if(strongVersion) {
                    //goes to a random place on the screen, and moves semi-randomly
                }
                else {
                    //appears on one side of the player, facing towards/moving towards them
                }
            }
        }
        //Will always move forward (unless hitting a wall)
        //Other abilities:

        //When collides with the player: it turns around

        //When basic version collides with a wall: goes underground

        //Advanced version will turn at different points
    }
}
