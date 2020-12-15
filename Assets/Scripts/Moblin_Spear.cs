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
        spearHit = Physics2D.Raycast(transform.position, transform.up * 1.5f, .5f);
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
}
