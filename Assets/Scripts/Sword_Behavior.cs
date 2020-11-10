using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Behavior : MonoBehaviour
{
    //i use these to decide which side the sword is facing
    public facingRight;
    public facingLeft;
    public facingUp;
    public facingDown;
    public canShootSword = true;
    public hasSword = true; //this can be adjusted. When getting the sword, this bool becomes true.
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.W)){
            facingUp = true;
            transform.Rotate(0f, 0f, 0f);
        } else {
            facingUp = false;
        }

        if (Input.GetKeyUp(KeyCode.X) && isNormalEnd == true){
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,255);
            Debug.Log("normalend");
        } else {
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            Debug.Log("noend");
        }
    }
}
