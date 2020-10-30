using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: allows the Peahat to fly around and avoid attacks
//USAGE: Attached to a Peahat (moth-like creature) Prefab
public class Enemy_Peahat : MonoBehaviour
{
    public bool flying;
    int peahatHealth = 2;
    float flyTime;
    float max_flyTime;
    Vector3 flyToSpot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //flies around semi-randomly - can fly over walls
        
        //after a period of time (which will be semi-random), gets tired and "sits down", at which point it can be attacked
    }
}
