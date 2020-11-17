using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public static int RoomNumber;
    public NPCSpawnerScript NPCSpawn;
    public ItemSpawnerScript ItemSpawn;
    public GameObject PlayerObject;
    public bool PlayerSpawned;

    // Start is called before the first frame update
    void Start()
    {
        // eventually create a list of roomnumbers here
        RoomNumber = 1;
        PlayerSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSpawned == false){
            Instantiate(PlayerObject, new Vector3 (0, -5.5f, 0), Quaternion.identity);
            PlayerSpawned = true;
        }
        // if room number = x , spawn the corresponding x npc and items
        // if you have the sword, don't spawn the sword again
        if (RoomNumber == 1){
            NPCSpawn.NPCNumber = 1;
            ItemSpawn.ItemSequenceNumber = 1;
        }
        if (RoomNumber == 2){
            NPCSpawn.NPCNumber = 2;
            ItemSpawn.ItemSequenceNumber = 2;
        }
        if (RoomNumber == 3){
            NPCSpawn.NPCNumber = 3;
            ItemSpawn.ItemSequenceNumber = 3;
        }
        if (RoomNumber == 4){
            NPCSpawn.NPCNumber = 4;
            ItemSpawn.ItemSequenceNumber = 4;
        }
        if (RoomNumber == 5){
            NPCSpawn.NPCNumber = 5;
            ItemSpawn.ItemSequenceNumber = 5;
        }
    }
}
