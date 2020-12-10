using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public ItemSlot[] slots;//Slot 0 = Sword, slot 1 = bomb, slot 2 = candle
    public Sprite[] spriteType;//Slot 0 = Sword, slot 1 = bomb, slot 2 = candle
    public ItemSlot bEquip;
    public ItemSlot bEquip2;
    public Text BombText;
    public PlayerControl myPlayer;
    public bool choosingEquip;
    B_Button myBButton;
    PlayerCombat myCombat;
    //Below are things which deal with the paused menu, moving around
    List<GameObject> PausedThings = new List<GameObject>();
    public RectTransform myInventoryBack;
    Vector3 inventoryBasePos;
    public Vector3 inventoryTruePos;
    void Start() {
        inventoryBasePos = myInventoryBack.position;
        for(int i = 0; i < slots.Length; i++) {
            slots[i].slotNumber = i;
        }
        myBButton = GetComponent<B_Button>();
        myPlayer = GetComponent<PlayerControl>();
        myCombat = GetComponent<PlayerCombat>();
    }
    public void fillSlot(int slot, int count, int type) {
        slots[slot].myImage.enabled = true;
        slots[slot].itemCount = count;
        slots[slot].itemType = type;
        if(type == 1) {
            BombText.text = count.ToString();
            myPlayer.orb = count;
            myPlayer.orb_slot = slot;
        }
        if(slot > 0) {
            slots[slot].myImage.sprite = spriteType[type];
            if(!bEquip.myImage.enabled) {
                bEquip.myImage.enabled = true;
                bEquip2.myImage.enabled = true;
                bEquip.myImage.sprite = slots[slot].myImage.sprite;
                bEquip2.myImage.sprite = slots[slot].myImage.sprite;
                bEquip.itemType = slots[slot].itemType;
                bEquip2.itemType = slots[slot].itemType;
                myBButton.equipped = slots[slot].itemType;
            }
        }
    }
    void Update() {
        //When ENTER is pressed, goes to a paused Inventory screen
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(!choosingEquip) {
                GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemies");
                foreach(GameObject enemy in Enemies) {
                    enemy.SetActive(false);
                    PausedThings.Add(enemy);
                }
                GameObject[] Spawners = GameObject.FindGameObjectsWithTag("Spawner");
                foreach(GameObject spawn in Spawners) {
                    spawn.SetActive(false);
                    PausedThings.Add(spawn);
                }
                myBButton.pause = true;
                myPlayer.pause = true;
                myCombat.pause = true;
                choosingEquip = true;
                myInventoryBack.localPosition = inventoryTruePos;
            }
            else {
                choosingEquip = false;
                myBButton.pause = false;
                myPlayer.pause = false;
                myCombat.pause = false;
                foreach(GameObject thing in PausedThings) {
                    thing.SetActive(true);
                }
                while(PausedThings.Count > 0) {
                    PausedThings.RemoveAt(0);
                }
                myInventoryBack.position = inventoryBasePos;
            }
        }
    }

}
