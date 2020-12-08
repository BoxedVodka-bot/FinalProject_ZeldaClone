using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public ItemSlot[] slots;//Slot 0 = Sword, slot 1 = bomb, slot 2 = candle
    public Sprite[] spriteType;//Slot 0 = Sword, slot 1 = bomb, slot 2 = candle
    public ItemSlot bEquip;
    B_Button myBButton;
    void Start() {
        for(int i = 0; i < slots.Length; i++) {
            slots[i].slotNumber = i;
        }
        myBButton = GetComponent<B_Button>();
    }
    public void fillSlot(int slot, int count, int type) {
        slots[slot].myImage.enabled = true;
        slots[slot].itemCount = count;
        slots[slot].itemType = type;
        if(slot > 0) {
            slots[slot].myImage.sprite = spriteType[type];
            if(!bEquip.myImage.enabled) {
                bEquip.myImage.enabled = true;
                bEquip.myImage.sprite = slots[slot].myImage.sprite;
                bEquip.itemType = slots[slot].itemType;
                myBButton.equipped = slots[slot].itemType;
            }
        }
    }

}
