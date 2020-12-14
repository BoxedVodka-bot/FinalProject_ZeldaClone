using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerScript : MonoBehaviour
{
    public int ItemSequenceNumber;
    public int item;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject Health;
    public GameObject Candle;
    public GameObject Bomb;
    public bool ItemsSpawned;
    public Vector3 spawn1;
    public Vector3 spawn2;
    public Vector3 spawn3;
    public Sword_Behavior SwordBehaviorScript;
    public PlayerControl PlayerControlScript;
    public SceneManagerScript sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        ItemSequenceNumber = 0;
        ItemsSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if the item sequence number is 1
        if (ItemSequenceNumber == 1 && ItemsSpawned == false && SwordBehaviorScript.hasSword == false){
            // sword room
            GameObject item = Instantiate(Sword, spawn2, Quaternion.identity);
            sceneManager.ItemPickUp = item.GetComponent<PickUp>();
            ItemsSpawned = true;           
        }
        if (ItemSequenceNumber == 2 && ItemsSpawned == false){
            // candle room
            GameObject item = Instantiate(Candle, spawn2, Quaternion.identity);
            sceneManager.ItemPickUp = item.GetComponent<PickUp>();
            ItemsSpawned = true;
        }
        if (ItemSequenceNumber == 3 && ItemsSpawned == false){
            // bomb room
            GameObject item = Instantiate(Bomb, spawn2, Quaternion.identity);
            sceneManager.ItemPickUp = item.GetComponent<PickUp>();
            ItemsSpawned = true;
        }
        if (ItemSequenceNumber == 4 && ItemsSpawned == false){
            // candle room
            GameObject item = Instantiate(Shield, spawn2, Quaternion.identity);
            sceneManager.ItemPickUp = item.GetComponent<PickUp>();
            ItemsSpawned = true;
        }
        if (ItemSequenceNumber == 5 && ItemsSpawned == false){
            // health room
            GameObject item = Instantiate(Health, spawn2, Quaternion.identity);
            sceneManager.ItemPickUp = item.GetComponent<PickUp>();
            ItemsSpawned = true;
        }
    }
}
