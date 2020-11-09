using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Used to explode the bomb item when placed, damaging nearby enemies
//USAGE: Attached to a BOMB Prefab
public class BombTick : MonoBehaviour
{
    public float startTickTime;
    float tickTime;
    public Transform explosionPrefab;
    
    void Start()
    {
        tickTime = startTickTime;
    }

    // Update is called once per frame
    void Update()
    {
        tickTime -= Time.deltaTime;
        if(tickTime <= 0) {
            //Destroy this, and create a set of simultaneous explosions
            Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));//at position
            Instantiate(explosionPrefab, transform.position + transform.right, Quaternion.Euler(0f, 0f, 0f));//+ x
            Instantiate(explosionPrefab, transform.position - transform.right, Quaternion.Euler(0f, 0f, 0f));//- x
            Instantiate(explosionPrefab, transform.position + new Vector3(0.5f, Mathf.Sqrt(3) / 2, 0f).normalized, Quaternion.Euler(0f, 0f, 0f));//+y, +x
            Instantiate(explosionPrefab, transform.position + new Vector3(0.5f, -Mathf.Sqrt(3) / 2f, 0f).normalized, Quaternion.Euler(0f, 0f, 0f));//-y, +x
            Instantiate(explosionPrefab, transform.position + new Vector3(-0.5f, Mathf.Sqrt(3) / 2, 0f).normalized, Quaternion.Euler(0f, 0f, 0f));//-y, +x
            Instantiate(explosionPrefab, transform.position + new Vector3(-0.5f, -Mathf.Sqrt(3) / 2, 0f).normalized, Quaternion.Euler(0f, 0f, 0f));//-y, -x
            Destroy(this.gameObject);
        }
    }
}
