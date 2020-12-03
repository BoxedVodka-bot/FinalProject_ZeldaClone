using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtomControl : MonoBehaviour
{   
    //PURPOSE: for menu after player's death
    //USAGE: Put this on the panal/canvas that have button children
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            if(!keyDown){
                if(index < maxIndex){
                    index++;
                } else {
                    index = 0;
                }
            }
            keyDown = true;
        } else {
            keyDown = false;
        }
    }
}
