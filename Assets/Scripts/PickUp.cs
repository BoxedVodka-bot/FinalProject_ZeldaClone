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
        if(other.CompareTag("Player")){
            for(int i = 0; i<inventory.slots.Length; i++){
                if(inventory.isFull[i] == false){
                    //item can be added
                    inventory.isFull[i] = true;
                    inventory.fillSlot(i, itemCount, itemType);
                    //Instantiate(itemInBox, inventory.slots[i].transform, false);
                    if(this.CompareTag("Sword")) {
                        Animator anim = other.GetComponent<Animator>();
                        other.GetComponent<PlayerCombat>().hasSword = true;
                        anim.SetTrigger("SwordPickup");
                    }
                    Destroy(this.gameObject);
                    break;
                }
                else if(inventory.slots[i].itemType == itemType) {
                    inventory.slots[i].itemCount += itemCount;
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
    }
}
