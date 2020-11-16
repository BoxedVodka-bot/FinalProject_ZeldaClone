using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //USAGE: put this on an item that can be picked up and added into the inventory

    private Inventory inventory;
    public GameObject itemInBox; //for instantiate items in the inventory box
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            for(int i = 0; i<inventory.slots.Length; i++){
                if(inventory.isFull[i] == false){
                    //item can be added
                    inventory.isFull[i] = true;
                    Instantiate(itemInBox, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
