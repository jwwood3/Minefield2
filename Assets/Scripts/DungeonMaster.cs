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
    private int curScore;
	public TextMeshProUGUI Score;
	public TextMeshProUGUI CheckPoint;
	public TextMeshProUGUI HighScore;

	public GameObject Enemy;
	public GameObject Player;
	public GameObject PowerUp;
    public GameObject PointPiece;
	// Use this for initialization
	void Start () {
		timer  = 0.0f;
		timer2 = 0.0f;
		timer3 = 0.0f;
        curScore  = 0;
        Statics.masterMind.pointCountdown = 5.0f;
        Statics.masterMind.powerUpCountdown = 20.0f;

    }

    public void scorePoints(int points)
    {
        curScore += points;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Statics.masterMind.gameState==2)//if you lost // this is set in Player_Script
		{
			if(curScore>0)
			{
				
				for(int i=0;i<Statics.masterMind.HighScores.Length;i++)
				{
					if(curScore>Statics.masterMind.HighScores[i])
					{
						for(int j=Statics.masterMind.HighScores.Length-1;j>i;j--)
						{
							Statics.masterMind.HighScores[j]=Statics.masterMind.HighScores[j-1];
						}
						Statics.masterMind.HighScores[i]=curScore;
						for(int j=Statics.masterMind.HighScorers.Length-1;j>i;j--)
						{
							Statics.masterMind.HighScorers[j]=Statics.masterMind.HighScorers[j-1];
						}
						Statics.masterMind.HighScorers[i]=Statics.masterMind.currentUser;
						break;
					}
				}
				
				
				Statics.masterMind.AddNewHighScore(Statics.masterMind.currentUser,curScore);
				
				Statics.masterMind.score=curScore;
                curScore = 0;
				timer=0.0f;
				timer2=0.0f;
				timer3=0.0f;
			}
			
		}
		else//if you're still playing
		{
			//display score
			
			timer+=Time.fixedDeltaTime;
            if (timer > 1)
            {
                curScore++;
                timer = timer-1.0f;
            }
			timer2+=Time.fixedDeltaTime;
			timer3+=Time.fixedDeltaTime;
			Score.text=curScore.ToString();
			HighScore.text=(Mathf.Floor(Statics.masterMind.HighScores[0]*10)/10).ToString();
			CheckPoint.text=(Statics.masterMind.phase).ToString();
			
			if(Statics.masterMind.x<=20)//WIP - resets x when the phase increases
			{
				Statics.masterMind.phase++;
				if(Statics.masterMind.phase>7)
				{
					Statics.masterMind.phase=7;
				}
				Statics.masterMind.x=60;
			}

            if (!Statics.masterMind.hasPowerUp)
            {
                Statics.masterMind.powerUpCountdown -= Time.fixedDeltaTime;
                if (Statics.masterMind.powerUpCountdown <= 0)
                {
                    Statics.masterMind.hasPowerUp = true;
                    Instantiate(PowerUp);
                }
            }

            Statics.masterMind.pointCountdown -= Time.fixedDeltaTime;
            if (Statics.masterMind.pointCountdown <= 0)
            {
                Instantiate(PointPiece);
                Statics.masterMind.pointCountdown = 5.0f;
            }

            //spawn balls every x frames
            if (timer3>(((float)Statics.masterMind.x)/60.0))
			{
				for(int n=0;n<Statics.masterMind.phase;n++)// 4 balls at start, 5 after checkpoint 1, etc.
				{
					Instantiate(Enemy);// spawn ball
				}
				timer3=0.0f;
			}
			
			if(timer2>1)//WIP - decrement x over time
			{
				Statics.masterMind.x-=Statics.masterMind.game;
				timer2=timer2-1.0f;
			}
		}
	}
}
