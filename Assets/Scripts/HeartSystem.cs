using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{

    public int maxHeartAmount = 3;
    public int startHearts = 3;
    public int curHealth;
    public int maxHealth;
    private int healthPerHeart = 2;

    public Image[] healthImages;
    public Sprite[] healthSprites;

    public GameObject deadState;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = startHearts * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;
        deadState.SetActive(false);
        checkHealthAmount();
    }

    public void checkHealthAmount(){
        for(int i = 0; i<maxHeartAmount; i++){
            if(startHearts<=i){
                healthImages[i].enabled = false;
            } else {
                healthImages[i].enabled = true;
            }
        }
        UpdateHearts();
    }

    void UpdateHearts(){
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages) {
            if(empty){
                image.sprite = healthSprites[0];
            } else {
                i++;
                if(curHealth >= i*healthPerHeart){
                    image.sprite = healthSprites[healthSprites.Length-1];
                } else {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart*i-curHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }

        }
        
        if (curHealth == 0){
            isDead = true;
            if (isDead == true){
                deadState.SetActive(true);
            }
            deadState.SetActive(true);
            //All possibilities are paused
            GetComponent<PlayerCombat>().pause = true;
            GetComponent<PlayerControl>().pause = true;
            GetComponent<B_Button>().pause = true;
            GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
    
    public void TakenDamage(int amount){
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, startHearts*healthPerHeart);
        UpdateHearts();
    }
    
}
