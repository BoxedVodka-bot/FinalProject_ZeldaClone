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
        //Have him hit enemies on start
        //Checks all colliders in its area
        Collider2D[] myColliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x - 0.1f);
        for(int i =0; i < myColliders.Length; i++) {
            if(myColliders[i].CompareTag("Enemies")) {
                Enemy_HP myEnemyHealth = myColliders[i].GetComponent<Enemy_HP>();
                if(!myEnemyHealth.invince) {
                    myEnemyHealth.health -= 5;
                    if(myEnemyHealth.health > 0) {
                        myEnemyHealth.invince = true;
                        myEnemyHealth.invincibleTime = myEnemyHealth.maxInvincibleTime;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        explodeTime -= Time.deltaTime;
        if(explodeTime <= 0f) {
            Destroy(this.gameObject);
        }
    }

    //BELOW: Resolved Issue, keeping it here for the sake of a contingency plan
    //This Trigger isn;t working? Might make it into an OverlapCircle
    //void OnTriggerStay2D(Collider2D activator) {
         //   Debug.Log("H");
       // if(activator.CompareTag("Enemy")) {
//            Enemy_HP myEnemyHealth = activator.GetComponent<Enemy_HP>();
        //        myEnemyHealth.health -= 3;
        //        myEnemyHealth.invince = true;
        //    }
     //   }
   // }//
}
