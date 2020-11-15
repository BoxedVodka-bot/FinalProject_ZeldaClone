using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerScript : MonoBehaviour
{
    public int ItemSequenceNumber;
    public int item;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject Key;
    public GameObject Candle;
    public GameObject Bomb;
    public GameObject Arrow;
    public bool ItemsSpawned;
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
        if (ItemSequenceNumber == 1 && ItemsSpawned == false){
            Instantiate(Sword, new Vector3 (0, -0.75f, 0), Quaternion.identity);
            ItemsSpawned = true;
        }
        if (ItemSequenceNumber == 2 && ItemsSpawned == false){
            Instantiate(Shield, new Vector3 (-2, -0.75f, 0), Quaternion.identity);
            Instantiate(Key, new Vector3 (0, -0.75f, 0), Quaternion.identity);
            Instantiate(Candle, new Vector3 (2, -0.75f, 0), Quaternion.identity);
            ItemsSpawned = true;
            // if you have x rupees, you can buy something
        }
        if (ItemSequenceNumber == 3 && ItemsSpawned == false){
            Instantiate(Shield, new Vector3 (-2, -0.75f, 0), Quaternion.identity);
            Instantiate(Bomb, new Vector3 (0, -0.75f, 0), Quaternion.identity);
            Instantiate(Arrow, new Vector3 (2, -0.75f, 0), Quaternion.identity);
            ItemsSpawned = true;
            // if you have x rupees, you can buy something
        }
        if (ItemSequenceNumber == 4 && ItemsSpawned == false){
            ItemsSpawned = true;
        }
        if (ItemSequenceNumber == 5 && ItemsSpawned == false){
            ItemsSpawned = true;
        }
    }
}
