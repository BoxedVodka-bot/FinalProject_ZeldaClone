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
    bool blocked;
    Vector3 direction;
    AudioSource myAudio;
    void Start()
    {
        direction = transform.up;
        myAudio = GetComponent<AudioSource>();
    }

    
    void Update()
    {   
        lifeTime -= Time.deltaTime;
        transform.position += direction * spearSpeed * Time.deltaTime;
        if(blocked) {
            transform.position += transform.right /2f * Time.deltaTime;
        }
        LayerMask mask = LayerMask.GetMask("PlayerCollision");
        spearHit = Physics2D.Raycast(transform.position, direction * 1.5f, .5f, mask);
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
                Destroy(gameObject);
            }
            else if(pControl.myCombat.attacking > 0) {
                pControl.EnemyCollision(transform.position, -1);
                Destroy(gameObject);
            }
            else {//Shield blocks enemy at correct angles
                Debug.Log("BLOCKED");
                blocked = true;
                spearSpeed *=-0.7f;
                lifeTime = 0.3f;
                Destroy(GetComponent<BoxCollider2D>());
                myAudio.Play();
            }
        }
    }
}
