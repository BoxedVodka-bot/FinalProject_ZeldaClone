using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moblin_Spear : MonoBehaviour
{
    // PURPOSE: move and destroy spear when it hits something, unless it hits shield,
    //          where it will bounce
    // USAGE: attach to moblin prefab


    public float spearSpeed;
    public RaycastHit2D spearHit;
    public float  lifeTime;
    void Start()
    {
        
    }

    
    void Update()
    {   
        lifeTime -= Time.deltaTime;
        transform.position += transform.up * spearSpeed * Time.deltaTime;
        LayerMask mask = LayerMask.GetMask("PlayerCollision");
        spearHit = Physics2D.Raycast(transform.position, transform.up * 1.5f, .5f, mask);
        if (spearHit.collider != null)
        {
            PlayerControl pControl = spearHit.collider.GetComponent<PlayerCollisionInfo>().myPlayerControl;
            pControl.EnemyCollision(transform.position, -1);
            Destroy(gameObject);
        }
        if(lifeTime < 0) {
            Destroy(gameObject);
        }
    }
    //Wasn't working so I added a trigger
    void OnTriggerEnter2D(Collider2D activator) {
        if(activator.CompareTag("PlayerCollision")) {
            Vector3 direction = transform.up;
            PlayerControl pControl = activator.GetComponent<PlayerCollisionInfo>().myPlayerControl;
            if(pControl.directionRecord != direction * -1) {
                pControl.EnemyCollision(transform.position, -1);
            }
            else if(pControl.myCombat.attacking > 0) {
                pControl.EnemyCollision(transform.position, -1);
            }
            else {
                Debug.Log("BLOCKED");
            }
            Destroy(gameObject);
        }
    }
}
