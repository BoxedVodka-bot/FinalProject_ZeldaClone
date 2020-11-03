using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    static int RoomNumber;

    // Start is called before the first frame update
    void Start()
    {
        // eventually create a list of roomnumbers here
        RoomNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // if you have the sword, don't spawn the sword again
        if (RoomNumber == 1){
            // if room number = x , spawn the corresponding x npc and items
            // move all npc and item scripts here? or keep separate? (ask how to manage variables between scripts)
        }
    }
}
