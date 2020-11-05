using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PURPOSE: control player character's combat movements
//USAGE: put this on a player character
public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public LayerMask enemyLayers;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            Attack();
        }
    }

    void Attack(){
        //Play an attack animation
        anim.SetTrigger("Attack");

        //Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    //Show attack range
    void OnDrawGizmosSelected(){

        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
