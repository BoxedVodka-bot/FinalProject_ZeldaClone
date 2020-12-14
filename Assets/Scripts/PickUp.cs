using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //USAGE: put this on an item that can be picked up and added into the inventory

    public int itemType;
    public int itemCount;
    private Inventory inventory;
    public GameObject itemInBox; //for instantiate items in the inventory box
    public int cost;
    public PlayerControl playerScript;
    public SceneManagerScript sceneManager;
    public NPCSpawnerScript NPCSpawn;
    TextMesh myText;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();
        NPCSpawn = sceneManager.NPCSpawn;
        if(cost > 0) {
            myText = gameObject.GetComponentInChildren<TextMesh>();
            myText.text = cost.ToString();
        }
    }

    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("PlayerCollision")){
            if(playerScript.diamond >= cost){ // if the player's diamond number is above the cost of an item
                playerScript.diamond -= cost; // buy the item by subtracting the cost of the item from the diamonds a player has
                playerScript.diamondNum.text = playerScript.diamond.ToString();
                //Plays audio if object has audio
                if(GetComponent<AudioSource>() != null) {
                    GetComponent<AudioSource>().Play();
                }
                if (NPCSpawn.NPCSpawned == true){ // if you are in a shop and buying items and the NPC is in the room...
                        NPCSpawn.NPCDisappear = true; // when you buy the item, that NPC disappears
                    }
                //for(int i = 0; i<inventory.slots.Length; i++){
                    if(itemType == 10) {//Heart purchase pickups have specific rules
                        if(playerScript.myHearts.curHealth < playerScript.myHearts.maxHealth) {
                            playerScript.myHearts.curHealth += 2;
                        if(playerScript.myHearts.curHealth > playerScript.myHearts.maxHealth) {
                            playerScript.myHearts.curHealth = playerScript.myHearts.maxHealth;
                        }
                        playerScript.myHearts.checkHealthAmount();
                        Destroy(gameObject);
            }
                    }
                    else if(inventory.isFull[itemType] == false){
                        //item can be added
                        inventory.isFull[itemType] = true;
                        inventory.fillSlot(itemType, itemCount, itemType);
                        //Instantiate(itemInBox, inventory.slots[i].transform, false);
                        if(this.CompareTag("Sword")) {
                            Animator anim = inventory.GetComponent<Animator>();
                            inventory.GetComponent<PlayerCombat>().hasSword = true;
                            anim.SetTrigger("SwordPickup");
                            inventory.GetComponent<PlayerControl>().pause = true;
                            inventory.GetComponent<PlayerControl>().pauseCause = this.gameObject;
                            PlayerCombat pCombat = inventory.GetComponent<PlayerCombat>();
                            pCombat.pause = true;
                            pCombat.mySword.hasSword = true;
                            inventory.GetComponent<B_Button>().pause = true;

                            Destroy(gameObject);
                        }
                        else if(inventory.slots[itemType].itemType == itemType) {
                            inventory.slots[itemType].itemCount += itemCount;
                            Destroy(gameObject);
                            //break;
                        }
                    }
                    else if(inventory.slots[itemType].itemType == itemType) {
                        inventory.slots[itemType].itemCount += itemCount;
                        if(itemType == 1) {
                            inventory.BombText.text = inventory.slots[itemType].itemCount.ToString();
                            inventory.myPlayer.orb = inventory.slots[itemType].itemCount;
                            inventory.myPlayer.orb_slot = itemType;
                        }
                        Destroy(gameObject);
                        //break;
                    }
                //}
            }
        }
    }
}
