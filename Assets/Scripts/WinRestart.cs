using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinRestart : MonoBehaviour
{

    //PURPOSE: options after winning the game
    //USAGE: put this on specific options under the win state parent
     [SerializeField] WinStateControl winStateControl;
    [SerializeField] int thisIndex;
    public GameObject selected;
    public string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if(winStateControl.index == thisIndex){
            selected.SetActive(false);
            if(winStateControl.index == 0){ // if choosing continue
                if(Input.GetKeyDown (KeyCode.Return)){
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name );
                } 
            }else if (winStateControl.index == 1) { // if choosing restart
                if(Input.GetKeyDown (KeyCode.Return)){
                    SceneManager.LoadScene(sceneToLoad);
                }
               
            }

        } else {
            selected.SetActive(true);
        }
    }
}
