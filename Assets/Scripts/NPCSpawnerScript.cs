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
    GameObject myNPC;
    public NpcDialogue NPCText;
    public Vector3 npcSpawnPosition;
    public bool NPCSpawned;
    public List<string> Text = new List<string>();
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
            myNPC = Instantiate(NPC1, npcSpawnPosition, Quaternion.identity);
            NPCSpawned = true;
            NPCText.myMonologue = Text[NPCNumber-1];
            NPCText.start = 1;
        }
        else if(NPCNumber == 2 && NPCSpawned == false){
            // instantiate the NPC 1
            myNPC = Instantiate(NPC2, npcSpawnPosition, Quaternion.identity);
            NPCSpawned = true;
            NPCText.myMonologue = Text[NPCNumber-1];
            NPCText.start = 1;
        }
        else if(NPCNumber == 3 && NPCSpawned == false){
            // instantiate the NPC 1
            myNPC = Instantiate(NPC2, npcSpawnPosition, Quaternion.identity);
            NPCSpawned = true;
            NPCText.myMonologue = Text[NPCNumber-1];
            NPCText.start = 1;
        }
        else if(NPCNumber == 4 && NPCSpawned == false){
            // instantiate the NPC 1
            myNPC = Instantiate(NPC3, npcSpawnPosition, Quaternion.identity);
            NPCSpawned = true;
            NPCText.myMonologue = Text[NPCNumber-1];
            NPCText.start = 1;
        }
        else if(NPCNumber == 5 && NPCSpawned == false){
            // instantiate the NPC 1
            myNPC = Instantiate(NPC3, npcSpawnPosition, Quaternion.identity);
            NPCSpawned = true;
            NPCText.myMonologue = Text[NPCNumber-1];
            NPCText.start = 1;
        }
        else if(NPCNumber == 0 && NPCSpawned == false) {
            Debug.Log("h");
            NPCSpawned = true;
            Destroy(myNPC);
            NPCText.myMonologue = "";
            NPCText.start = 1;
            NPCText.DeleteText();
            NPCText.GetComponent<Text>().text = "";
        }
    }
}
