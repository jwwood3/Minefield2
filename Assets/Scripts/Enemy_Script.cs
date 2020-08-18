using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour {
	public Vector3 target;
	public float speed;
	public GameObject Player;
	public Vector3 spawnPos;
	private Vector3 newPos;
	private Vector3 tempVector;
	// Use this for initialization
	void Start () {
		speed = 0.5f;
		//Set Position to a random open spawn Point
		Vector3 temp = new Vector3(0,0,0);
		int temp2 = Random.Range(0,Statics.masterMind.phase);
		temp.x = Statics.masterMind.spawnPoints[temp2,0];
		temp.y = Statics.masterMind.spawnPoints[temp2,1];
		this.transform.position = temp;
		//Set aim toward the player
		spawnPos = this.transform.position;// stores the spawn position for later use
		Player = GameObject.Find("Player_Sprite");// locates the player
		target = new Vector3(Random.Range(-8.0f,8.0f),Player.transform.position.y,0);// find the player's current position
		//target = Statics.unitVectorize(target);//Normalize the vector
		newPos = new Vector3(((this.transform.position.x-target.x)),((this.transform.position.y-target.y)),0);
		newPos = Statics.unitVectorize(newPos);
		newPos.x*=(speed*(Vector3.Distance(target,spawnPos)+0.1f)*Time.deltaTime);
		newPos.y*=(speed*(Vector3.Distance(target,spawnPos)+0.1f)*Time.deltaTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Statics.masterMind.gameState!=2)
		{
			tempVector = newPos;
			tempVector.x=this.transform.position.x-tempVector.x;
			tempVector.y=this.transform.position.y-tempVector.y;
			this.transform.position=tempVector;
			if(this.transform.position.x<Statics.masterMind.cameraCorners[0,0]||this.transform.position.x>Statics.masterMind.cameraCorners[1,0]||this.transform.position.y>Statics.masterMind.cameraCorners[0,1]||this.transform.position.y<Statics.masterMind.cameraCorners[1,1])
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
		}
	}
}
