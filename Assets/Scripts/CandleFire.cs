using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: the candle fire moves forward slowly, then destroys itself. It might collide with something
//USAGE: Attached to a candle fire prefab
public class CandleFire : MonoBehaviour
{
    public Vector3 direction; // The direction the fire moves in
    public float speed;
    public float maxLife;
    float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0) {
            Destroy(this.gameObject);
        }
        //Acts the same way no matter what: goes forward for a half its lifetime, and stays still for the second half
        if(lifeTime >= maxLife / 2) {
            transform.position += direction *Time.deltaTime;
        }
    }
    //When the player or an enemy entires the fire, they get damaged
    void OnTriggerEnter2D(Collider2D activator) {
        if(activator.CompareTag("PlayerCollision") && lifeTime <= maxLife * 0.75f) {//Only damages player after it has been moving for a bit
            //Player takes damage and is pushed back
            PlayerControl pControl = activator.GetComponent<PlayerCollisionInfo>().myPlayerControl;
            pControl.EnemyCollision(transform.position, -1);
        }
        else if(activator.CompareTag("Enemies")) {
            Enemy_HP enemyHP = activator.GetComponent<Enemy_HP>();
            enemyHP.TakeDamage(-1, false, transform.up);
            //Enemies are not pushed back, but they do take 1 damage
        }
    }
}
