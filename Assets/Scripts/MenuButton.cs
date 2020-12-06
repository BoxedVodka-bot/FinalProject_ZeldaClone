using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//PURPOSE: manage the indication animation & what each button can do. Use with MenuButtomControl
//USAGE: Put this on the buttons
public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtomControl menuButtomControl;
    [SerializeField] PlayerControl playerControl;
    [SerializeField] HeartSystem heartSystem;
    [SerializeField] int thisIndex;
    public GameObject selected;
    public GameObject menu;
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(menuButtomControl.index == thisIndex){
            selected.SetActive(false);
            if(menuButtomControl.index == 0){
                if(Input.GetKeyDown (KeyCode.Return)){
                    playerControl.transform.position = new Vector3 (0, 0, 0); //teleport player back to the beginning position
                    heartSystem.gameObject.GetComponent<HeartSystem>().TakenDamage(6); //restore full health
                    menu.SetActive(false);
                    heartSystem.isDead = false; //cancel the death state, allow player to move
                    inventory.SetActive(true);


                } 
            }else if (menuButtomControl.index == 1) {
                if(Input.GetKeyDown (KeyCode.Return)){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name );
                }
               
            }

        } else {
            selected.SetActive(true);
        }
    }
}