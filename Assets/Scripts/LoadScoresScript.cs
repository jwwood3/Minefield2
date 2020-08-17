using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadScoresScript : MonoBehaviour {
	public TextMeshProUGUI[] scoreLabels = new TextMeshProUGUI[10];
	public TextMeshProUGUI[] scorerLabels = new TextMeshProUGUI[10];
    public TextMeshProUGUI ButtonLabel;
	public TextMeshProUGUI place;
	public bool isLocal=true;
    void Start()
    {
        getScoreLabels();
        refresh(1);
    }

    public void getScoreLabels()
    {
        for(int i = 1; i <= 10; i++)
        {
            scoreLabels[i - 1] = GameObject.Find(i.ToString() + "_Score").GetComponent<TextMeshProUGUI>();
            scorerLabels[i - 1] = GameObject.Find(i.ToString() + "User_Label").GetComponent<TextMeshProUGUI>();
        }
    }
	
	
	public void refresh(int includeOnline)
	{
        if (includeOnline == 1)
        {
            Statics.masterMind.downloadHighscores();
        }
        for (int i = 0; i < scoreLabels.Length; i++)
        {
            if (isLocal)
            {
                if (Statics.masterMind.HighScores.Length > i && Statics.masterMind.HighScorers.Length > i)
                {
                    scoreLabels[i].text = ((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[i] * 10)) / 10.0f).ToString();
                    scorerLabels[i].text = Statics.masterMind.HighScorers[i];
                }
                else
                {
                    scoreLabels[i].text = "-";
                    scorerLabels[i].text = "-----";
                }

            }
            else if(includeOnline==1)
            {
                if (Statics.masterMind.scoreList.Length > i)
                {
                    scoreLabels[i].text = ((float)(Mathf.FloorToInt(Statics.masterMind.scoreList[i].score * 10)) / 10.0f).ToString();
                    scorerLabels[i].text = Statics.masterMind.scoreList[i].username;
                }
                else
                {
                    scoreLabels[i].text = "-";
                    scorerLabels[i].text = "-----";
                }
            }
        }
        if (isLocal)
        {
            int places = Statics.masterMind.findPlayerInLocal(Statics.masterMind.currentUser);
            if (places == 11)
            {
                place.text = ">10th";
            }
            else
            {
                place.text = Statics.intToString(places);
            }
        }
        else
        {
            int places = Statics.masterMind.findPlayerInRankings(Statics.masterMind.currentUser);
            if (places == 1001)
            {
                place.text = ">1000th";
            }
            else
            {
                place.text = Statics.intToString(places);
            }
        }
    }
	
	public void switchLists()
	{
		isLocal=!isLocal;
		if(isLocal)
		{
			ButtonLabel.text="Online";
		}
		else
		{
			ButtonLabel.text="Local";
		}
        refresh(1);
	}
	/*public TextMeshProUGUI first;
	public TextMeshProUGUI second;
	public TextMeshProUGUI third;
	public TextMeshProUGUI fourth;
	public TextMeshProUGUI fifth;
	public TextMeshProUGUI firstUser;
	public TextMeshProUGUI secondUser;
	public TextMeshProUGUI thirdUser;
	public TextMeshProUGUI fourthUser;
	public TextMeshProUGUI fifthUser;
	// Use this for initialization
	void Start () {
		first.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[0]*10))/10.0f).ToString();
		firstUser.text=Statics.masterMind.HighScorers[0];
		second.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[1]*10))/10.0f).ToString();
		secondUser.text=Statics.masterMind.HighScorers[1];
		third.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[2]*10))/10.0f).ToString();
		thirdUser.text=Statics.masterMind.HighScorers[2];
		fourth.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[3]*10))/10.0f).ToString();
		fourthUser.text=Statics.masterMind.HighScorers[3];
		fifth.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[4]*10))/10.0f).ToString();
		fifthUser.text=Statics.masterMind.HighScorers[4];
		sixth.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[5]*10))/10.0f).ToString();
		sixthUser.text=Statics.masterMind.HighScorers[5];
		seventh.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[6]*10))/10.0f).ToString();
		seventhUser.text=Statics.masterMind.HighScorers[6];
		eighth.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[7]*10))/10.0f).ToString();
		eighthUser.text=Statics.masterMind.HighScorers[7];
		ninth.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[8]*10))/10.0f).ToString();
		ninthUser.text=Statics.masterMind.HighScorers[8];
		tenth.text=((float)(Mathf.FloorToInt(Statics.masterMind.HighScores[9]*10))/10.0f).ToString();
		tenthUser.text=Statics.masterMind.HighScorers[9];
	}*/
	
	public void menu()
	{
		SceneManager.LoadScene("Menu");
	}
	
	// Update is called once per frame
	void Update () {
        refresh(0);
	}
}
