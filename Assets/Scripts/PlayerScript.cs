using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int healthCount;
    public static int rupeeCount;
    public static int bombCount;
    public static bool HasSword;
    // Start is called before the first frame update
    void Start()
    {
        healthCount = 6;
        rupeeCount = 10;
        bombCount = 0;
        HasSword = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // pick ups
        // if you collide with something of a tag "sword", you unlock the sword
        if (col.gameObject.CompareTag("Sword") && (HasSword = false))
        {
            HasSword = true;
            Debug.Log("the player has the sword");
        }
        // if you collide with something of a tag "health", you gain health
        if (col.gameObject.CompareTag("Heart") && (healthCount < 6))
        {
            healthCount = healthCount + 1;
            Debug.Log("player gains half a heart");
        }
        // if you collide with something of a tag "rupee", you gain rupees
        if (col.gameObject.CompareTag("BlueRupee"))
        {
            rupeeCount = rupeeCount + 5;
            Debug.Log("player gains a rupee");
        }
        if (col.gameObject.CompareTag("YellowRupee"))
        {
            rupeeCount = rupeeCount + 1;
            Debug.Log("player gains a rupee");
        }
        // if you collide with something of a tag "bomb", you gain a bomb
        if (col.gameObject.CompareTag("Bomb"))
        {
            bombCount = bombCount + 1;
            Debug.Log("player gains a bomb");
        }
        // more collissions here for different items you gain

        // buying items
        if (col.gameObject.CompareTag("ItemHeart")){
            // player static rupee score must go down by a number in the item/scene manager script that is the price of the item
            rupeeCount --;
            healthCount = healthCount + 1;
        }
    }
}