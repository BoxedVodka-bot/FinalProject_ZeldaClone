using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: the candle fire moves forward slowly, then destroys itself. It might collide with something
//USAGE: Attached to a candle fire prefab
public class CandleFire : MonoBehaviour
{
    public Vector3 direction; // The direction the fire moves in
    float speed;
    float lifeTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0) {
            Destroy(this.gameObject);
        }
    }
}
