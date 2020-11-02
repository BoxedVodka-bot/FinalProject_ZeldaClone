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
}
