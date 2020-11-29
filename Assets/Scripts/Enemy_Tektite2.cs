using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//PURPOSE: Re-done code to try to get the Tektite jumping to work, gonna start simple then expand
//USAGE: Attached to a tektite prefab
public class Enemy_Tektite2 : MonoBehaviour
{
    bool jumping;//Bool that says whethere enemy is jumping
    public float speed;//Speed of the Tektite while jumping
    float timeToJump;
    float jumpTime;
    public float maxTimeToJump;
    public float maxJumpTime;
    Camera myCamera;
    Enemy_HP myHP;
    Transform myPlayer;
    Vector3 jumpDirection;
    Tilemap myTilemap;
    public int statBarOffest;
    public float spawning;//used to determine how long it takes for the enemy to be prepared befor they can actually act
    // Start is called before the first frame update
    void Start()
    {
        timeToJump = Random.Range(0f, maxTimeToJump);
        myHP = GetComponent<Enemy_HP>();
        myCamera = myHP.myCamera;
        myPlayer = myHP.myPlayer;
        myTilemap = myHP.myTilemap;
        List<Vector3> spawnPlaces = new List<Vector3>();
        //get a list of tiles this guy can spawn on
        //For loop of y values, than x values
        for(int i = 1; i < myCamera.orthographicSize * 2 - statBarOffest - 1f; i++) {
            for(int j = 1; j < myCamera.orthographicSize * 2 * myCamera.aspect - 2f; j++ ) {
                //x position is equal to bottom left corner plus j
                int x = (int)(myCamera.transform.position.x - myCamera.orthographicSize * myCamera.aspect) + j; 
                //y position is equal to bottom left corner plus i
                int y = (int)(myCamera.transform.position.y - myCamera.orthographicSize) + i;
                //Check the tile at that position
                Tile myTile = myTilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                if(myTile == null) {
                    spawnPlaces.Add(new Vector3((float)x + 0.5f, (float)y, 0f));
                } 
            }
        }
        if(spawnPlaces.Count > 0) {
            int rnd = Random.Range(0, spawnPlaces.Count - 1);
            //This will need to be changed a bit so that the enemy doesn't spawn too close to the player
            transform.position = spawnPlaces[rnd];
        }
    }

    // Update is called once per frame
    void Update()
    {
    if(spawning > 0) {
        spawning -=Time.deltaTime;
        transform.eulerAngles += new Vector3(0f, 0f, 15f);//This is going to be replaced with this units different sprite
        if(spawning <= 0) {
            spawning = 0;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);//going to be replaced with changing to correct sprite
            
        }
    }
    else {
        if(!jumping) {
            timeToJump -= Time.deltaTime;
            if(timeToJump <= 0) {
                timeToJump = Random.Range(0f, maxTimeToJump);
                jumping = true;
                jumpDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
                jumpTime = Random.Range(maxJumpTime * 2 / 3, maxJumpTime);
                //Also, should jump in the general direction of the player if close enough
                float distToPlayer = Mathf.Abs(Vector3.Magnitude(transform.position - myPlayer.position));
                if(distToPlayer < speed * jumpTime / 2) {//If it is close enough to reach the player, they might jump towards them
                    float rnd = Random.Range(0f, 1f);
                    if(rnd < 0.5f) {
                        //They jump towards the player, with a little bit of variation
                        jumpDirection = myPlayer.transform.position - transform.position;
                        jumpDirection = (jumpDirection + new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(-0.5f, 0.5f), 0f)).normalized;
                        jumpDirection = new Vector3(jumpDirection.x, jumpDirection.y, 0f);
                    }
                }
                //Checks to make sure they won't jump outside boundaries
                if(jumpDirection.x > 0) {
                    if(jumpDirection.x * jumpTime * speed + transform.position.x > myCamera.transform.position.x + myCamera.aspect * myCamera.orthographicSize - 1f) {
                        jumpDirection = new Vector3(-jumpDirection.x, jumpDirection.y, 0f);
                    }
                }
                else {
                    if(jumpDirection.x * jumpTime * speed + transform.position.x < myCamera.transform.position.x - myCamera.aspect * myCamera.orthographicSize + 1f) {
                        jumpDirection = new Vector3(-jumpDirection.x, jumpDirection.y, 0f);
                    }
                }
                if(jumpDirection.y > 0) {
                    if(jumpDirection.y * jumpTime * speed + transform.position.y > myCamera.transform.position.y + myCamera.orthographicSize - 1f - (float)statBarOffest) {
                        jumpDirection = new Vector3(jumpDirection.x, -jumpDirection.y, 0f);
                    }
                }
                else {
                    if(jumpDirection.y * jumpTime * speed + transform.position.y < myCamera.transform.position.y - myCamera.orthographicSize + 1f) {
                        jumpDirection = new Vector3(jumpDirection.x, -jumpDirection.y, 0f);
                    }
                }
            }
        }
        else {
            jumpTime -= Time.deltaTime;
            if(jumpTime <= 0) {
                jumping = false;
            }
            else {
                //This is gonna end up being fixed to jump in an arc, but at the moment its straight
                transform.position += jumpDirection * Time.deltaTime * speed;
            }
        }
    }
    }
}
