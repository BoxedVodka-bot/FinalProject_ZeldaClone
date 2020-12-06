using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//PURPOSE: control scene change
//USAGE: Put this in the startscreen
public class StartScreen : MonoBehaviour
{
    public string sceneToLoad;
    
    void Update()
    {
       if (Input.anyKey){
                SceneManager.LoadScene(sceneToLoad);
            } 
    }
}
