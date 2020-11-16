using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerScript : MonoBehaviour
{
    public int ItemSequenceNumber;
    public int item;
    public GameObject Sword;
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
            Instantiate(Sword, new Vector3 (0, -2, 0), Quaternion.identity);
            ItemsSpawned = true;
        }
    }
}
