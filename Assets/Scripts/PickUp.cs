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
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("J");
        if(other.CompareTag("PlayerCollision")){
            for(int i = 0; i<inventory.slots.Length; i++){
                if(inventory.isFull[i] == false){
                    //item can be added
                    inventory.isFull[i] = true;
                    inventory.fillSlot(i, itemCount, itemType);
                    //Instantiate(itemInBox, inventory.slots[i].transform, false);
                    if(this.CompareTag("Sword")) {
                        Animator anim = inventory.GetComponent<Animator>();
                        inventory.GetComponent<PlayerCombat>().hasSword = true;
                        anim.SetTrigger("SwordPickup");
                        inventory.GetComponent<PlayerControl>().pause = true;
                        inventory.GetComponent<PlayerControl>().pauseCause = this.gameObject;
                        inventory.GetComponent<PlayerCombat>().pause = true;
                        inventory.GetComponent<B_Button>().pause = true;
                    }
                    Destroy(this.gameObject);
                    break;
                }
                else if(inventory.slots[i].itemType == itemType) {
                    inventory.slots[i].itemCount += itemCount;
                    if(itemType == 1) {
                        inventory.BombText.text = inventory.slots[i].itemCount.ToString();
                        inventory.myPlayer.orb = inventory.slots[i].itemCount;
                        inventory.myPlayer.orb_slot = i;
                    }
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
