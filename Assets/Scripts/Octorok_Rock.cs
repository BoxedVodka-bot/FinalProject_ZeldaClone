using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octorok_Rock : MonoBehaviour
{
    // PURPOSE: Move and destroy rock when it hits something
    // USAGE: Attach to rock prefab


    public float rockSpeed;
    public RaycastHit2D rockHit;

    // create a Vector2 variable to determine which direction the rock is going to fly
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * rockSpeed * Time.deltaTime;

        rockHit = Physics2D.Raycast(transform.position, transform.up * 1.5f, .5f);

        if (rockHit.collider != null)
        {
            Destroy(gameObject);
        }
    }
}
