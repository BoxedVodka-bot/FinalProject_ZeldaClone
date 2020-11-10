using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    static int RoomNumber;
    public NPCSpawnerScript NPCSpawn;
    public ItemSpawnerScript ItemSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // eventually create a list of roomnumbers here
        RoomNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // if room number = x , spawn the corresponding x npc and items
        // if you have the sword, don't spawn the sword again
        if (RoomNumber == 1){
            NPCSpawn.NPCNumber = 1;
            ItemSpawn.ItemSequenceNumber = 1;
        }
    }
}
