using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//PURPOSE: Code for the water enemies that shoot at the player
//USAGE: Attached to a Zora Prefab
public class Enemy_Zora : MonoBehaviour
//May have fixed this issue below, needs more testing
//ISSUES: Currently has an issue where it can spawn off the screen
{
    //This enemy's health
    int zoraHealth = 2;
    //The time this enemy spends above the surface of the water
    float surfaceTime = 0;
    public float max_surfaceTime;
    //The time this enemy spends below the surface of the water
    float diveTime = 0;
    public float max_diveTime;
    //Whether or not this enemy has surfaced
    public bool hasSurfaced = true;
    //A boolean used for when the Zora needs to move to a new random place
    bool positionReset;
    SpriteRenderer mySpriteRenderer;
    public Transform myPlayer;
    Enemy_HP myHP;
    //This enemy's projectile
    public Transform myFireballPrefab;
    //The delay to shoot the projectile after surfacing
    float shotDelay = 0;
    public float max_shotDelay;
    //Whether or not the enemy needs to shoot a projectile
    bool shoot = false;
    public Camera myCamera;
    public int statBarOffest;//used for the stat bar at the top of the screen - need to find a better way to do this
    //These Tilemaps below are used to determine where this object can spawn
    public Animator anim;
    public Tilemap myTilemap;
    public List<Vector3> waterTiles  = new List<Vector3>();
    public List<Sprite> possibleWaterTiles = new List<Sprite>();//List of possible water sprites this creature can spawn on
    BoxCollider2D myCollider;//The collider box of this entity - activates and deactivates at different times
    void Start()
    {
        anim = GetComponent<Animator>();
        myHP = GetComponent<Enemy_HP>();
        myCollider = GetComponent<BoxCollider2D>();
        myCamera = myHP.myCamera;
        myTilemap = myHP.myTilemap;
        myPlayer = myHP.myPlayer;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //get a list of tiles this guy can spawn on
        //For loop of y values, than x values
        for(int i = 0; i < myCamera.orthographicSize * 2 - statBarOffest; i++) {
            for(int j = 0; j < myCamera.orthographicSize * 2 * myCamera.aspect - 1; j++ ) {
                //x position is equal to bottom left corner plus j
                int x = (int)(myCamera.transform.position.x - myCamera.orthographicSize * myCamera.aspect) + j; 
                //y position is equal to bottom left corner plus i
                int y = (int)(myCamera.transform.position.y - myCamera.orthographicSize) + i;
                //Check the tile at that position
                Tile myTile = myTilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                if(myTile != null) {
                    for(int k = 0; k< possibleWaterTiles.Count; k++) {
                        if(myTile.sprite == possibleWaterTiles[k]) {
                            Debug.Log(x.ToString() + " " + y.ToString());
                            waterTiles.Add(new Vector3((float)x + 0.5f, (float)y, 0f));//y might need to have 0.5 added to it
                        } 
                    }
                } 
            }
        }
        //If there is at least 1 water tile, this enemy is teleported to 1 of them when it spawns
        if(waterTiles.Count > 0) {
            int rnd = Random.Range(0, waterTiles.Count - 1);
            transform.position = waterTiles[rnd];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasSurfaced) {
            surfaceTime += Time.deltaTime;
            if(surfaceTime >= max_surfaceTime) {
                anim.SetBool("IsUnderWater", true);
                myCollider.enabled = false;
                myHP.invince = true;
                myHP.health = zoraHealth;
                positionReset = false;
                hasSurfaced = false;
                surfaceTime = 0;
            }
        }
        else {
            diveTime += Time.deltaTime;
            if(diveTime > max_diveTime) {
                anim.SetBool("IsUnderWater", false);
                myCollider.enabled = true;
                myHP.invince = false;
                diveTime = 0;
                hasSurfaced = true;
                shoot = true;
            }
        }
        if(shoot) {
            shotDelay +=Time.deltaTime;
            if(shotDelay >= max_shotDelay ) {
                shotDelay = 0;
                shoot = false;
                Transform fireball = Instantiate(myFireballPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));
                Zora_FireballMove myFire = fireball.GetComponent<Zora_FireballMove>();
                myFire.myPlayer = myPlayer;
            }
        }
        //Halfway through its respawn, the Zora's position is reset
        if(diveTime > max_diveTime / 2 && !positionReset) {
            positionReset = true;
            //Gonna probably use tilemapping for this
            int rnd = Random.Range(0, waterTiles.Count - 1);
            transform.position = waterTiles[rnd];
        }
    }
}
