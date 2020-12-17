using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octorok_Rock : MonoBehaviour
{
    // PURPOSE: Move and destroy rock when it hits something
    // USAGE: Attach to rock prefab


    public float rockSpeed;
    float timeLeft;
    public RaycastHit2D rockHit;

    // create a Vector2 variable to determine which direction the rock is going to fly
    public Vector3 direction;
    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.up;
        timeLeft = 0;
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0) {
            timeLeft -=Time.deltaTime;
            if(timeLeft <=0) {
                Destroy(gameObject);
            }
        }
        transform.position += direction * rockSpeed * Time.deltaTime;
        rockHit = Physics2D.Raycast(transform.position, transform.up, 0.4f);
        Debug.DrawRay(transform.position, transform.up * 0.4f, Color.magenta);

        if (rockHit.collider != null)
        {
            if(rockHit.collider.CompareTag("Wall")) {
                Destroy(gameObject);
            }
            else if(rockHit.collider.CompareTag("PlayerCollision")) {
                Debug.Log("HITPLAYER");
                //Player takes damage
                PlayerControl pControl = rockHit.collider.GetComponent<PlayerCollisionInfo>().myPlayerControl;
                pControl.EnemyCollision(transform.position, -1);
                Destroy(gameObject);
            }
            else if(rockHit.collider.CompareTag("Player")) {
                Debug.Log("HITPLAYER");
                //Player takes damage
                PlayerControl pControl = rockHit.collider.GetComponent<PlayerControl>();
                pControl.EnemyCollision(transform.position, -1);
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D activator) {
        if(activator.CompareTag("PlayerCollision")) {
            PlayerControl pControl = activator.GetComponent<PlayerCollisionInfo>().myPlayerControl;
            if(pControl.directionRecord != direction * -1) {
                pControl.EnemyCollision(transform.position, -1);
                Destroy(gameObject);
            }
            else if(pControl.myCombat.attacking > 0) {
                pControl.EnemyCollision(transform.position, -1);
                Destroy(gameObject);
            }
            else {
                Debug.Log("BLOCKED");
                direction = -(direction + transform.right / 2f) * 0.7f;
                timeLeft = 0.3f;
                myAudio.Play();
                Destroy(GetComponent<BoxCollider2D>());
            }
        }
    }
}
