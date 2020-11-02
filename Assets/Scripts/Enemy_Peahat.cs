﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: allows the Peahat to fly around and avoid attacks
//USAGE: Attached to a Peahat (moth-like creature) Prefab
public class Enemy_Peahat : MonoBehaviour
{
    public bool flying;
    public float baseSpeed;
    float speed;
    int peahatHealth = 2;
    float flyTime;//How long the Peahat has been flying for
    float sitTime;
    public float max_sitTime;
    public float max_flyTime;
    bool slowDown;
    bool speedUp;
    float timeStraight;
    public float max_timeStraight;
    Vector3 flyDirection;//The direction this Peahat is currently flying in (determined semi-randomly)

    void Start()
    {
        speed = baseSpeed;
        flying = true;
        flyDirection = new Vector3(1f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //flies around semi-randomly - can fly over walls
        if(flying) {
        timeStraight-=Time.deltaTime;
        float rnd = Random.Range(0f, 1f);
        if(timeStraight <= 0 ) {
            if(rnd < 0.015f) {
                flyDirection = new Vector3(1f, 0f, 0f);
                timeStraight = Random.Range(0.5f, max_timeStraight);
            }
            else if(rnd < 0.03f) {
                flyDirection = new Vector3(0f, 1f, 0f);
                timeStraight = Random.Range(0.5f, max_timeStraight);
            }
            else if(rnd < 0.045f) {
                flyDirection = new Vector3(-1f, 0f, 0f);
                timeStraight = Random.Range(0.5f, max_timeStraight);
            }
            else if(rnd < 0.06f) {
                flyDirection = new Vector3(0f, -1f, 0f);
                timeStraight = Random.Range(0.5f, max_timeStraight);
            }
            else if(rnd < 0.075f) {
                flyDirection = new Vector3(1f, 1f, 0f).normalized;
                timeStraight = Random.Range(0.5f, max_timeStraight);
            }
            else if(rnd < 0.09f) {
                flyDirection = new Vector3(1f, -1f, 0f).normalized;
                timeStraight = Random.Range(0.5f, max_timeStraight);
            }
            else if(rnd < 0.105f) {
                flyDirection = new Vector3(-1f, 1f, 0f).normalized;
                timeStraight = Random.Range(0.5f, max_timeStraight);
            }
            else if(rnd < 0.12f) {
                flyDirection = new Vector3(-1f, -1f, 0f).normalized;
                timeStraight = Random.Range(0.5f, max_timeStraight);
            }
            else {
                //timeStraight-=0.5f;
            }
        }
        //If the peahat's position + their direction * speed would be off screen, they turn around
        

        transform.position += flyDirection * speed *Time.deltaTime;
        flyTime+=Time.deltaTime;
        if(flyTime >= max_flyTime) {
            slowDown = true;
        }
        }
        else {
            sitTime += Time.deltaTime;
            if(sitTime >= max_sitTime) {
                sitTime = 0f;
                speedUp = true;
                flying = true;
            }
        }
        if(speedUp) {
            speed += Time.deltaTime;
            if(speed >= baseSpeed) {
                speed = baseSpeed;
                speedUp = false;
            }
        }
        if(slowDown) {
            speed -= Time.deltaTime;
            if(speed <=0) {
                speed = 0;
                flyTime = Random.Range(0f, max_flyTime / 4);
                slowDown = false;
                flying = false;
            }
        }
        //after a period of time (which will be semi-random), gets tired and "sits down", at which point it can be attacked
    }
}
