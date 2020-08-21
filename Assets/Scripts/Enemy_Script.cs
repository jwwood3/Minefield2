using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class Enemy_Script : MonoBehaviour
{
    public Vector3 target;
    public float speed;
    public float noise;
    int index;
    public GameObject Player;
    private Vector3 newPos;
    private Vector3 tempVector;
    // Use this for initialization
    void Start()
    {
        speed = ((2.0f*Mathf.Pow((Statics.masterMind.currentScore/100.0f),2.0f))/100.0f)+Statics.masterMind.enemySpeed;
        Player = GameObject.Find("Player_Sprite");// locates the player
        if (Statics.target(index))
        {
            
            target = Player.transform.position;// find the player's current position
        }
        else
        {
            //TODO: make these coordinates references - Find x value function
            target = new Vector3((float)((Random.value * 8) - 4), Player.transform.position.y, 0);// Target random spot on the player's x plane
            //target = new Vector3((float)((Random.value * noise) - (noise/2))+Player.transform.position.x, Player.transform.position.y, 0);// Target random spot on the player's x plane
        }

        newPos = new Vector3(((this.transform.position.x - target.x)), ((this.transform.position.y - target.y)), 0);
        newPos = Statics.unitVectorize(newPos);
        newPos.x *= (speed * (Vector3.Distance(target, this.transform.position) + 0.1f) * Time.deltaTime);
        newPos.y *= (speed * (Vector3.Distance(target, this.transform.position) + 0.1f) * Time.deltaTime);
    }

    public void setIndex(int i)
    {
        index = i;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Statics.masterMind.gameState != 2)
        {
            tempVector = newPos;
            tempVector.x = this.transform.position.x - tempVector.x;
            tempVector.y = this.transform.position.y - tempVector.y;
            this.transform.position = tempVector;
            if (this.transform.position.x < Statics.masterMind.cameraCorners[0, 0] || this.transform.position.x > Statics.masterMind.cameraCorners[1, 0] || this.transform.position.y > Statics.masterMind.cameraCorners[0, 1] || this.transform.position.y < Statics.masterMind.cameraCorners[1, 1])
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
        }
    }
}
