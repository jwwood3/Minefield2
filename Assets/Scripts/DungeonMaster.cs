using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DungeonMaster : MonoBehaviour {
	private float timer;
	private float timer2;
	private float timer3;
	public TextMeshProUGUI Score;
	public TextMeshProUGUI CheckPoint;
	public TextMeshProUGUI HighScore;

	public GameObject Enemy;
	public GameObject Player;
	public GameObject PowerUp;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
		timer2=0.0f;
		timer3=0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Statics.masterMind.gameState==2)//if you lost // this is set in Player_Script
		{
			if(timer>0)
			{
				
				for(int i=0;i<Statics.masterMind.HighScores.Length;i++)
				{
					if(timer>Statics.masterMind.HighScores[i])
					{
						for(int j=Statics.masterMind.HighScores.Length-1;j>i;j--)
						{
							Statics.masterMind.HighScores[j]=Statics.masterMind.HighScores[j-1];
						}
						Statics.masterMind.HighScores[i]=timer;
						for(int j=Statics.masterMind.HighScorers.Length-1;j>i;j--)
						{
							Statics.masterMind.HighScorers[j]=Statics.masterMind.HighScorers[j-1];
						}
						Statics.masterMind.HighScorers[i]=Statics.masterMind.currentUser;
						break;
					}
				}
				/*if(timer>Statics.masterMind.HighScores[0])
				{
					Statics.masterMind.HighScores[4]=Statics.masterMind.HighScores[3];
					Statics.masterMind.HighScores[3]=Statics.masterMind.HighScores[2];
					Statics.masterMind.HighScores[2]=Statics.masterMind.HighScores[1];
					Statics.masterMind.HighScores[1]=Statics.masterMind.HighScores[0];
					Statics.masterMind.HighScores[0]=timer;
					Statics.masterMind.HighScorers[4]=Statics.masterMind.HighScorers[3];
					Statics.masterMind.HighScorers[3]=Statics.masterMind.HighScorers[2];
					Statics.masterMind.HighScorers[2]=Statics.masterMind.HighScorers[1];
					Statics.masterMind.HighScorers[1]=Statics.masterMind.HighScorers[0];
					Statics.masterMind.HighScorers[0]=Statics.masterMind.currentUser;
				}
				else if(timer>Statics.masterMind.HighScores[1])
				{
					Statics.masterMind.HighScores[4]=Statics.masterMind.HighScores[3];
					Statics.masterMind.HighScores[3]=Statics.masterMind.HighScores[2];
					Statics.masterMind.HighScores[2]=Statics.masterMind.HighScores[1];
					Statics.masterMind.HighScores[1]=timer;
					Statics.masterMind.HighScorers[4]=Statics.masterMind.HighScorers[3];
					Statics.masterMind.HighScorers[3]=Statics.masterMind.HighScorers[2];
					Statics.masterMind.HighScorers[2]=Statics.masterMind.HighScorers[1];
					Statics.masterMind.HighScorers[1]=Statics.masterMind.currentUser;
				}
				else if(timer>Statics.masterMind.HighScores[2])
				{
					Statics.masterMind.HighScores[4]=Statics.masterMind.HighScores[3];
					Statics.masterMind.HighScores[3]=Statics.masterMind.HighScores[2];
					Statics.masterMind.HighScores[2]=timer;
					Statics.masterMind.HighScorers[4]=Statics.masterMind.HighScorers[3];
					Statics.masterMind.HighScorers[3]=Statics.masterMind.HighScorers[2];
					Statics.masterMind.HighScorers[2]=Statics.masterMind.currentUser;
				}
				else if(timer>Statics.masterMind.HighScores[3])
				{
					Statics.masterMind.HighScores[4]=Statics.masterMind.HighScores[3];
					Statics.masterMind.HighScores[3]=timer;
					Statics.masterMind.HighScorers[4]=Statics.masterMind.HighScorers[3];
					Statics.masterMind.HighScorers[3]=Statics.masterMind.currentUser;
				}
				else if(timer>Statics.masterMind.HighScores[4])
				{
					Statics.masterMind.HighScores[4]=timer;
					Statics.masterMind.HighScorers[4]=Statics.masterMind.currentUser;
				}*/
				
				Statics.masterMind.AddNewHighScore(Statics.masterMind.currentUser,timer);
				
				Statics.masterMind.score=timer;
				timer=0.0f;
				timer2=0.0f;
				timer3=0.0f;
			}
			
		}
		else//if you're still playing
		{
			//display score
			if(!Statics.masterMind.hasPowerUp)
			{
				Statics.masterMind.powerUpCountdown-=Time.deltaTime;
				if(Statics.masterMind.powerUpCountdown<=0)
				{
					Statics.masterMind.hasPowerUp=true;
					Instantiate(PowerUp);
				}
			}
			timer+=Time.deltaTime;
			timer2+=Time.deltaTime;
			timer3+=Time.deltaTime;
			Score.text=(Mathf.Floor(timer*10)/10).ToString();
			HighScore.text=(Mathf.Floor(Statics.masterMind.HighScores[0]*10)/10).ToString();
			CheckPoint.text=(Statics.masterMind.phase-4).ToString();
			
			if(Statics.masterMind.x==20)//WIP - resets x when the phase increases
			{
				Statics.masterMind.phase++;
				if(Statics.masterMind.phase>9)
				{
					Statics.masterMind.phase=9;
				}
				Statics.masterMind.x=60;
			}
			
			//spawn balls every x frames
			if(timer3>(((float)Statics.masterMind.x)/60.0))
			{
				for(int n=0;n<Statics.masterMind.phase;n++)// 4 balls at start, 5 after checkpoint 1, etc.
				{
					Instantiate(Enemy);// spawn ball
				}
				timer3=0.0f;
			}
			
			if(timer2>1)//WIP - decrement x over time
			{
				Statics.masterMind.x--;
				timer2=0.0f;
			}
			
			
			
			
		}
	}
}
