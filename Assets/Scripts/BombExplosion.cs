using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Used for the bomb's explosion, to deal damage to enemies and then disappear
//USAGE: Attached to the BombExplosion Prefab
public class BombExplosion : MonoBehaviour
{
    public float startExplodeTime;
    float explodeTime;

    // Start is called before the first frame update
    void Start()
    {
        explodeTime = startExplodeTime;
    }

    // Update is called once per frame
    void Update()
    {
        explodeTime -= Time.deltaTime;
        if(explodeTime <= 0f) {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D activator) {
        if(activator.CompareTag("Enemy")) {
            Enemy_HP myEnemyHealth = activator.GetComponent<Enemy_HP>();
            if(!myEnemyHealth.invince) {
                myEnemyHealth.health -= 3;
                myEnemyHealth.invince = true;
            }
        }
    }
}
