using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: create screen scrolling between each part of the map
//USAGE: put this on the main camera
public class CameraControl : MonoBehaviour
{
    public Transform target; //target pointing to the camera/player
    public Vector2 size; //size of the grid
    public float speed; //speed of transition. has to be a value between 0 and 1.
    // Update is called once per frame
    void Update()
    {
        //check if player position is within the size & move the camera
        Vector3 pos = new Vector3( Mathf.RoundToInt(target.position.x / size.x) * size.x, Mathf.RoundToInt(target.position.y / size.y) * size.y, transform.position.z);
        //make the transition smoother
        transform.position = Vector3.Lerp(transform.position, pos, speed);
    }
}
