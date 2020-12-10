using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//PURPOSE: Used to switch between Candle and Bomb, the two secondary objects
//USAGE: attached to a UI object in the inventory screen
public class EquipChoose : MonoBehaviour
{
    RectTransform myRect;
    public Inventory myInventory;
    public B_Button myBButton;
    int equipslot = 1;
    public List<RectTransform> Slot = new List<RectTransform>();
    // Start is called before the first frame update
    void Start()
    {
        myRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myInventory.choosingEquip) {
            //Press left/right arrow keys to switch equip - needs to check if its allowed
            if(Input.GetKeyDown(KeyCode.LeftArrow) && equipslot == 2) {
                if(myInventory.isFull[1]) {
                    equipslot = 1;
                    myBButton.charge = 0;
                    myRect.position = Slot[0].position;
                    myBButton.equipped = myInventory.slots[1].itemType;
                    myInventory.bEquip.myImage.sprite = myInventory.slots[1].myImage.sprite;
                    myInventory.bEquip.myImage.enabled = true;
                    myInventory.bEquip2.myImage.sprite = myInventory.slots[1].myImage.sprite;
                    myInventory.bEquip2.myImage.enabled = true;
                    if(myBButton.currentBomb != null) {
                        myBButton.currentBomb.gameObject.SetActive(false);
                    }
                }
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow) && equipslot == 1) {
                if(myInventory.isFull[2]) {
                    equipslot = 2;
                    myBButton.charge = 0;
                    myRect.position = Slot[1].position;
                    myBButton.equipped = myInventory.slots[2].itemType;
                    myInventory.bEquip.myImage.sprite = myInventory.slots[2].myImage.sprite;
                    myInventory.bEquip.myImage.enabled = true;
                    myInventory.bEquip2.myImage.sprite = myInventory.slots[2].myImage.sprite;
                    myInventory.bEquip2.myImage.enabled = true;
                    if(myBButton.currentBomb != null) {
                        myBButton.currentBomb.gameObject.SetActive(true);
                    }
                }
            }


        }
    }
}
