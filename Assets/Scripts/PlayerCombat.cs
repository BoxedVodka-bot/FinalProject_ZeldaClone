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
    B_Button myBButton;
    public Sword_Behavior mySword;//The player's sword
    float attacking;
    public bool hasSword;
    public AudioSource mySwordSound;

    void Start() {
        myControl = GetComponent<PlayerControl>();
        myBButton = GetComponent<B_Button>();
    }   
    // Update is called once per frame
    void Update()
    {
        if(!pause && hasSword) {
        if (Input.GetKeyDown(KeyCode.X) && attacking == 0){
            attacking = 0.7f;
            myControl.pause = true;
            myBButton.pause = true;
            myControl.pauseCause = this.gameObject;
            mySwordSound.Play();
            anim.SetTrigger("Attack");
            //Attack();
        }
        if(attacking > 0) {
            
            attacking -= Time.deltaTime;
            if(attacking <= 0) {
                attacking = 0;
                if(myControl.pauseCause = this.gameObject) {
                    myControl.pause = false;
                    myBButton.pause = false;
                }
            }
        }
        }
    }

    public void Attack() {
        //Play an attack animation
        //anim.SetTrigger("Attack");
        mySword.ThrowSword();//Tries to throw the sword, if they can
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
