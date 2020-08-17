using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame_Script : MonoBehaviour {
	public GameObject Player;
	public GameObject PlayingStuff;
	public GameObject GameOverStuff;
	public TMP_InputField playerName;
	
	

	
	public void startGame()
	{
		Statics.masterMind.gameState=1;
		Statics.masterMind.hasPowerUp=false;
		Statics.masterMind.powerUpCountdown=20;
		SceneManager.LoadScene("Main");
	}
	
	public void setPlayer()
	{
		Statics.masterMind.currentUser=playerName.text;
	}
	
	public void menu()
	{
		SceneManager.LoadScene("Menu");
	}
	
	public void menuFromGame()
	{
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Winner"))
		{
			Destroy(obj);
		}
		Player.transform.position=new Vector3(0,0,0);
		Statics.masterMind.gameState=0;
		PlayingStuff.SetActive(true);
		GameOverStuff.SetActive(false);
		SceneManager.LoadScene("Menu");
	}
	
	public void scores()
	{
		SceneManager.LoadScene("HighScores");
	}
	
	public void scoresFromGame()
	{
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Winner"))
		{
			Destroy(obj);
		}
		Player.transform.position=new Vector3(0,0,0);
		Statics.masterMind.gameState=0;
		PlayingStuff.SetActive(true);
		GameOverStuff.SetActive(false);
		SceneManager.LoadScene("HighScores");
	}
	
	public void playAgain()
	{
		#if UNITY_STANDALONE
		Cursor.visible=false;
		#endif
		Debug.Log("playAgain");
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			Destroy(obj);
		}
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Winner"))
		{
			Destroy(obj);
		}
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("PowerUp"))
		{
			Destroy(obj);
		}
		Player.transform.position=new Vector3(0,0,0);
		PlayingStuff.SetActive(true);
		GameOverStuff.SetActive(false);
		Statics.masterMind.gameState=1;
		Statics.masterMind.hasPowerUp=false;
		Statics.masterMind.powerUpCountdown=20;
		
	}
	
	public void quitGame()
	{
		Application.Quit();
	}
}
