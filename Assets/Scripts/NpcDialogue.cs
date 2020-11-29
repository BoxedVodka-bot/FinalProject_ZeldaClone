using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//PURPOSE: Monologue/Dialogue function where the NPC talks to the player
//USAGE: Attached to a text box in the NPC Room
public class NpcDialogue : MonoBehaviour
{
    public string myMonologue;//The NPCs monologue
    public string monologueSpoken;//The current monologue spoken
    public float wordTimeDelay;//Delay between letters spoken
    float currentDelay;
    public Text myText;
    List<char> myMonologueList = new List<char>();
    public int start;
    public PlayerControl myPlayerControl;
    public B_Button myPlayerB;
    public PlayerCombat myPlayerCombat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start == 1) {
            for(int i=0; i<myMonologue.Length; i++) {
                myMonologueList.Add(myMonologue[i]);
            }
            currentDelay = wordTimeDelay;
            start = 2;
            //Pause isn't working rn, but I can't do anything about it
            myPlayerB.pause = true;
            myPlayerControl.pause = true;
            myPlayerCombat.pause = true;
            myPlayerControl.pauseCause = this.gameObject;
        }
        else if(start == 2) {
            currentDelay -= Time.deltaTime;
            if(currentDelay <= 0 && myMonologueList.Count > 0) {
                currentDelay = wordTimeDelay;
                monologueSpoken += myMonologueList[0];
                myMonologueList.RemoveAt(0);
                Debug.Log(monologueSpoken);
                myText.text = monologueSpoken;
                myPlayerB.pause = true;
                myPlayerControl.pause = true;
                myPlayerCombat.pause = true;
                myPlayerControl.pauseCause = this.gameObject;
                myPlayerControl.anim.speed = 0;
            }
            if(myText.text == myMonologue) {
                start = 3;
                if(myPlayerControl.pauseCause = this.gameObject) {
                    myPlayerB.pause = false;
                    myPlayerControl.pause = false;
                    myPlayerCombat.pause = false;
                    myPlayerControl.anim.speed = 1;
                }
            }
        }
    }
    //Still doesn't completely work for some reason
    public void DeleteText() {
        while(myMonologueList.Count > 0) {
            myMonologueList.RemoveAt(0);
        }
        string str = "";
        monologueSpoken = str;
        myMonologue = str;
        myText.text = monologueSpoken;
    }
}
