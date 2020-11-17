using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSpawnerScript : MonoBehaviour
{
    public int NPCNumber;
    public GameObject NPC1;
    public GameObject NPC2;
    public GameObject NPC3;
    public Text NPCText;
    public bool NPCSpawned;
    public string Text;
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
            Instantiate(NPC1, new Vector3(0, 0.5f, 0), Quaternion.identity);
            NPCSpawned = true;
        }
        if(NPCNumber == 2 && NPCSpawned == false){
            // instantiate the NPC 1
            Instantiate(NPC2, new Vector3(0, 0.5f, 0), Quaternion.identity);
            NPCSpawned = true;
        }
        if(NPCNumber == 3 && NPCSpawned == false){
            // instantiate the NPC 1
            Instantiate(NPC2, new Vector3(0, 0.5f, 0), Quaternion.identity);
            NPCSpawned = true;
        }
        if(NPCNumber == 4 && NPCSpawned == false){
            // instantiate the NPC 1
            Instantiate(NPC3, new Vector3(0, 0.5f, 0), Quaternion.identity);
            NPCSpawned = true;
        }
        if(NPCNumber == 5 && NPCSpawned == false){
            // instantiate the NPC 1
            Instantiate(NPC3, new Vector3(0, 0.5f, 0), Quaternion.identity);
            NPCSpawned = true;
        }
    }
}
