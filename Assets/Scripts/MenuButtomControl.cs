using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtomControl : MonoBehaviour
{   
    //PURPOSE: for menu after player's death
    //USAGE: Put this on the panal/canvas that have button children
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public GameObject color1;
    public GameObject color2;
    public GameObject color3;
    public GameObject color4;
    public GameObject gameOverText;
    public GameObject continueOption;
    public GameObject restartOption;
    public GameObject heartIcon;
    public GameObject inventory;

    //Decide if player can press space (to open inventory)
    public bool isInputEnabled = true;
    [SerializeField] HeartSystem heartSystem;
    
    //Enable the following objects everytime player's dead
    void OnEnable()
    {
        StartCoroutine(Color1Show());
        StartCoroutine(Color2Show());
        StartCoroutine(Color3Show());
        StartCoroutine(Color4Show());
        StartCoroutine(Options());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift)){
            if(!keyDown){
                if(index < maxIndex){
                    index++;
                } else {
                    index = 0;
                }
            }
            keyDown = true;
        } else {
            keyDown = false;
        }
    }

    //First filter
    public IEnumerator Color1Show(){
        yield return new WaitForSeconds(1f);
        color1.SetActive(true);
        
        isInputEnabled = false; //player can not press space to open inventory
    }

    //Second filter
    public IEnumerator Color2Show(){
        yield return new WaitForSeconds(2f);
        color1.SetActive(false);
        color2.SetActive(true);
    }

    //Third filter
    public IEnumerator Color3Show(){
        yield return new WaitForSeconds(3f);
        color2.SetActive(false);
        color3.SetActive(true);
    }

    //Fourth filter & game over text shown
    public IEnumerator Color4Show(){
        yield return new WaitForSeconds(3.5f);
        color3.SetActive(false);
        color4.SetActive(true);
        gameOverText.SetActive(true);
    }

    //Continue and restart options shown
    public IEnumerator Options(){
        yield return new WaitForSeconds(5.5f);
        gameOverText.SetActive(false);
        inventory.SetActive(false);
        continueOption.SetActive(true);
        restartOption.SetActive(true);
        heartIcon.SetActive(true);

        isInputEnabled = true; //player can press SPACE again
    }

    

}
