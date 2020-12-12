using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//PURPOSE: Used to manage a single slot in the player's inventory
//USAGE: attached to a slot object, which is invisible
//Everything which uses ItemSlots should reference this for simplicity sake
public class ItemSlot : MonoBehaviour
{
    public Image myImage;
    public int slotNumber;
    public int itemType;
    public int itemCount;
    void Start() {
        myImage = GetComponent<Image>();
        myImage.enabled = false;
    }
}
