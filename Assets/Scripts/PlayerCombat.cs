using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PURPOSE: control player character's combat movements
//USAGE: put this on a player character
public class PlayerCombat : MonoBehaviour
{
    public bool pause;    
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.2f;
    public int attackDamage = 1;
    public LayerMask enemyLayers;
    PlayerControl myControl;
    float attacking;
    public bool hasSword;

    void Start() {
        myControl = GetComponent<PlayerControl>();
    }   
    // Update is called once per frame
    void Update()
    {
        if(!pause && hasSword) {
        if (Input.GetKeyDown(KeyCode.X) && attacking == 0){
            Attack();
        }
        if(attacking > 0) {
            attacking -= Time.deltaTime;
            if(attacking <= 0) {
                attacking = 0;
                myControl.pause = false;
            }
        }
        }
    }

    void Attack() {
        attacking = 1f;
        myControl.pause = true;
        //Play an attack animation
        anim.SetTrigger("Attack");

        //Detect enemies in range of attack
        attackPoint.position = transform.position + myControl.directionRecord;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);//, enemyLayers);
        Debug.DrawLine(transform.position, attackPoint.position);
        //Damage them
        foreach(Collider2D enemy in hitEnemies){
            //enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            if(enemy.CompareTag("Enemies")) {
                //Enemy takes damage
                Enemy_HP enemyHP = enemy.GetComponent<Enemy_HP>();
                enemyHP.TakeDamage(-attackDamage, true, myControl.directionRecord);
            }
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
