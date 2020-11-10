using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerScript : MonoBehaviour
{
    public int NPCNumber;
    public GameObject NPC;
    public bool NPCSpawned;
    // Start is called before the first frame update
    void Start()
    {
        NPCNumber = 0;
        NPCSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if the npc number is 1
        if(NPCNumber == 1 && NPCSpawned == false){
            // instantiate the NPC 1
            Instantiate(NPC, new Vector3(0, 0, 0), Quaternion.identity);
            NPCSpawned = true;

        }
    }
}
