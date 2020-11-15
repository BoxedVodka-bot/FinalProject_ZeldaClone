using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public int newRoomNumber;
    public static int RoomNumber;
    public NPCSpawnerScript NPCSpawn;
    public ItemSpawnerScript ItemSpawn;

    // Start is called before the first frame update
    void Start()
    {
        newRoomNumber = RoomNumber;
        // eventually create a list of roomnumbers here
        //RoomNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
        if(newRoomNumber == 1) {
            RoomNumber = 1;
        }
        // if room number = x , spawn the corresponding x npc and items
        // if you have the sword, don't spawn the sword again
        if (RoomNumber == 1){
            Debug.Log("!");
            //NPCSpawn.NPCNumber = 1;
           // ItemSpawn.ItemSequenceNumber = 1;
        }
    }
}
