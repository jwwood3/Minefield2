using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;

public class Statics : MonoBehaviour {
    public readonly float[,] spawnPoints = { { 1.075f, 4.5f }, { -1.075f, 4.5f }, { 0.0f, 4.5f }, { 2.15f, 4.5f }, { -2.15f, 4.5f }};//Array of spawn Points
    public int minX;
    public int x;
    public readonly float[,] cameraCorners = { {-15,15}, {15,-15} };//Coords of topLeft and bottomRight corners of FOV for destroying offScreen objects.
    public int phase;
    public int game = 1;
	public int gameState =1;//0:menu 1:playing 2:gameOver
	public float[] HighScores = {0,0,0,0,0,0,0,0,0,0};
	public string[] HighScorers = {"","","","","","","","","",""};
	public float score;
	public bool hasLoadedYet=false;
	public string currentUser="Player 1";
    public float powerUpCountdown;
    public float pointCountdown;
	public static Statics masterMind;

    //DevFlags
    public float itemBuffer = 0.2f;//How long after item spawn can items be picked up
    public int minPhase = 2;//Number of spawns at the beginning of the game
    public float phaseLength = 30.0f;//How long before another spawner appears and spawn rate resets
    public float pointDuration = 2.5f;//How long point pickups remain on the screen before despawning
    public float powerUpDuration = 1.5f;//"  "   power up   "     "    "  "     "      "        "
    public int pointValue = 5;//Starting point value of point pickups
    public float pointFactor = 12.0f;//multiplier in logarithmic equation for point value growth as level increases
    public float spawnFactor = 8.0f;//multiplier in equation for spawn chance as phase increases
    public int spawnChance = 100;//Base spawn chance in phase 1 at the beginning of the phase
    public int startX = 20;//Starting value for minX and x, x balls spawn every startX seconds, so higher means spawn rate increases more slowly and vice versa
    public int minXInc = 5;//Increment for minX when level increases
    public float defPUC = 20.0f;//Time in between power up spawns
    public float defPC = 5.0f;//Time in between point spawns
    public float enemySpeed = 0.5f;//Base speed of enemy bullets




    //http://dreamlo.com/lb/XyD8-wun_EyBXa0yIHZYcQaBBghpoZN0-W26oPg9ShQA
    private static string privateCode = "FJcB7d7fZUK_o86S-DCB2wz9oTLNP0BUGzj9Bxk-80GA";
	private static string publicCode = "5f3b70bdeb371809c4d63863";
	private static string webURL = "http://dreamlo.com/lb/";
	public Highscore[] scoreList;
	
	
	void Awake()
	{
		if(masterMind==null)
		{
			print("shouldn't destroy on load");
			DontDestroyOnLoad(gameObject);
			masterMind=this;
		}
		else if(masterMind!=this)
		{
			Destroy(gameObject);
		}
	}
	
	void Start()
	{
        resetStaticStuff();
        fillTextBoxes();
		print("scorelength:"+Statics.masterMind.HighScores.Length);
		print("scorerlength:"+Statics.masterMind.HighScorers.Length);
        

	}

    public void fillTextBoxes()
    {
        print("FunctionRunning");
        if (!(GameObject.Find("DevMenuTitle") is null))
        {
            print("ActuallyDoingStuff");
            GameObject.Find("itemBufferField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = itemBuffer.ToString();
            GameObject.Find("minPhaseField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = minPhase.ToString();
            GameObject.Find("phaseLengthField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = phaseLength.ToString();
            GameObject.Find("pointDurationField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = pointDuration.ToString();
            GameObject.Find("powerUpDurationField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = powerUpDuration.ToString();
            GameObject.Find("minPointsField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = pointValue.ToString();
            GameObject.Find("pointFactorField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = pointFactor.ToString();
            GameObject.Find("spawnFactorField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = spawnFactor.ToString();
            GameObject.Find("baseSpawnField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = spawnChance.ToString();
            GameObject.Find("startXField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = startX.ToString();
            GameObject.Find("minXIncField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = minXInc.ToString();
            GameObject.Find("defPUCField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = defPUC.ToString();
            GameObject.Find("defPCField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = defPC.ToString();
            GameObject.Find("enemySpeedField/Text Area/Placeholder").GetComponent<TextMeshProUGUI>().text = enemySpeed.ToString();
        }
    }

    public void resetStaticStuff()
    {
        this.powerUpCountdown = this.defPUC;
        this.pointCountdown = this.defPC;
        this.minX = this.startX;
        this.x = this.minX;
        this.phase = this.minPhase;
    }

    

    public void AddNewHighScore(string username, int scorel)//adds highscore to online database
	{
		this.StartCoroutine(UploadNewHighscore(username,scorel));
	}
	
	IEnumerator UploadNewHighscore(string username, int scored)//uploads a score to the online database (called by AddNewHighScore function)
	{
		WWW www = new WWW(webURL + privateCode + "/add/"+WWW.EscapeURL(username)+"/"+scored);
		yield return www;
		if(string.IsNullOrEmpty(www.error))
		{
			print("Upload Successful");
		}
		else
		{
			print(www.error);
		}
	}
	
	public void downloadHighscores()//downloads online scores
	{
		this.StartCoroutine(DownloadHighscores());
	}
	
	void formatHighscores(string text)//formats online scores to a usable format
	{
		string[] entries = text.Split(new char[] {'\n'},System.StringSplitOptions.RemoveEmptyEntries);
		scoreList=new Highscore[entries.Length];
		for(int i=0;i<entries.Length;i++)
		{
			string[] entryinfo = entries[i].Split(new char[] {'|'});
			string username = entryinfo[0];
			float score = ((float)int.Parse(entryinfo[1]));
			scoreList[i]=new Highscore(username,score);
			print(scoreList[i].username+":"+scoreList[i].score);
		}
	}
	public struct Highscore{//data structure to hold an online high score
		public string username;
		public float score;
		public Highscore(string user, float sc)
		{
			username=user;
			score=sc;
		}
	}
	
	IEnumerator DownloadHighscores()//downloads online scores
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;
		if(string.IsNullOrEmpty(www.error))
		{
			formatHighscores(www.text);
		}
		else
		{
			print(www.error);
		}
	}

	void OnApplicationPause(bool check)//saves locally when the app stops
	{
		if(hasLoadedYet)
		{
			Save();
		}
		else{
			Load();
		}
	}
	void OnApplicationQuit()//saves locally when the app stops (doesn't work for android)
	{
		Save();
	}
	
	public static Vector3 unitVectorize(Vector3 input)//normalizes vectors to get a direction
    {
        float mag = Mathf.Sqrt((input.x*input.x)+(input.y*input.y)+(input.z*input.z));
		if(mag==0)
		{
			return new Vector3(0,0,0);
		}
		else
		{
			return new Vector3((input.x/mag),(input.y/mag),(input.z/mag));
		}
    }
	public static void resetGame()//opens the game scene
	{
		SceneManager.LoadSceneAsync("Main");
	}
	
	public static void endGame(float score)//opens the game over scene
	{
		SceneManager.LoadSceneAsync("GameOver");
	}
	public static void toMenu()//opens the menu scene
	{
		SceneManager.LoadSceneAsync("Menu");
	}
	
	public void Save()//saves high scores locally
	{
		Debug.Log("Saving");
		BinaryFormatter format = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/HighScores.dat");
		Saver saveData = new Saver(HighScores,HighScorers,currentUser);
		format.Serialize(file,saveData);
		file.Close();
		Debug.Log("Saved");
	}
	
	public void Load()//loads local high scores
	{
		Debug.Log("Loading");
		if(File.Exists(Application.persistentDataPath + "/HighScores.dat") && !hasLoadedYet)
		{
			Debug.Log("Found File");
			BinaryFormatter format = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/HighScores.dat",FileMode.Open);
			Saver saveData = (Saver)format.Deserialize(file);
			file.Close();
			if(HighScores.Length<=saveData.HighScores.Length)
			{
				HighScores=saveData.HighScores;
			}
			else
			{
				for(int i=0;i<saveData.HighScores.Length;i++)
				{
					HighScores[i]=saveData.HighScores[i];
				}
				for(int j=saveData.HighScores.Length;j<HighScores.Length;j++)
				{
					HighScores[j]=0;
				}
			}
			if(HighScorers.Length<=saveData.HighScorers.Length)
			{
				HighScorers=saveData.HighScorers;
			}
			else
			{
				for(int i=0;i<saveData.HighScorers.Length;i++)
				{
					HighScorers[i]=saveData.HighScorers[i];
				}
				for(int j=saveData.HighScorers.Length;j<HighScorers.Length;j++)
				{
					HighScorers[j]="";
				}
			}
			currentUser=saveData.lastUser;
			Debug.Log("Loaded");
		}
		hasLoadedYet=true;
	}
	
	[System.Serializable]
	class Saver//data structure to hold save data before saving to a file
	{
		public float[] HighScores;
		public string[] HighScorers;
		public string lastUser;
		public Saver(float[] hs, string[] people, string ls)
		{
			HighScores=hs;
			HighScorers=people;
			lastUser=ls;
		}
	}
	
	public int findPlayerInRankings(string user)
	{
		for(int i=0;i<scoreList.Length;i++)
		{
			if(scoreList[i].username==user)
			{
				return (i+1);
			}
		}
		return 1001;
	}
	
	public int findPlayerInLocal(string user)
	{
		for(int i=0;i<HighScorers.Length;i++)
		{
			if(HighScorers[i]==user)
			{
				return (i+1);
			}
		}
		return 11;
	}
	
	public static string intToString(int k)
	{
		int i=k%10;
		int j=k%100;
		int jk=Mathf.FloorToInt((((float)(j))/10));
		string added="";
		switch(i)
		{
			case 1:
				if(jk!=1)
				{
					added="st";
				}
				else
				{
					added="th";
				}
				break;
			case 2:
				if(jk!=1)
				{
					added="nd";
				}
				else
				{
					added="th";
				}
				break;
			case 3:
				if(jk!=1)
				{
					added="rd";
				}
				else
				{
					added="th";
				}
				break;
			default:
				added="th";
				break;
		}
		return (k.ToString()+added);
	}
}
