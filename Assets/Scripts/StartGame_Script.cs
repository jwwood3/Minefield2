using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame_Script : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayingStuff;
    public GameObject GameOverStuff;
    public TMP_InputField playerName;


    public void resetStuff()
    {

        Statics.masterMind.powerUpCountdown = Statics.masterMind.defPUC;
        Statics.masterMind.pointCountdown = Statics.masterMind.defPC;
        Statics.masterMind.phase = Statics.masterMind.minPhase;
        Statics.masterMind.game = 1;
        Statics.masterMind.x = Statics.masterMind.startX;
        Statics.masterMind.minX = Statics.masterMind.startX;
    }

    public void startGame()
    {
        resetStuff();
        Statics.masterMind.gameState = 1;
        SceneManager.LoadScene("MainGame");
    }

    public void setPlayer()
    {
        Statics.masterMind.currentUser = playerName.text;
    }

    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void devMenu()
    {
        SceneManager.LoadScene("DevMenu");
    }

    public void fillTextBoxes()
    {
        Statics.masterMind.fillTextBoxes();
    }

    public void setItemBuffer(string newBuffer)
    {
        Statics.masterMind.itemBuffer = float.Parse(newBuffer);
        Statics.masterMind.resetStaticStuff();
    }

    public void setMinPhase(string newMinPhase)
    {
        Statics.masterMind.minPhase = int.Parse(newMinPhase);
        Statics.masterMind.resetStaticStuff();
    }

    public void setPhaseLength(string newPhaseLength)
    {
        Statics.masterMind.phaseLength = float.Parse(newPhaseLength);
        Statics.masterMind.resetStaticStuff();
    }

    public void setPointDuration(string newPointDuration)
    {
        Statics.masterMind.pointDuration = float.Parse(newPointDuration);
        Statics.masterMind.resetStaticStuff();
    }

    public void setPowerUpDuration(string newPowerUpDuration)
    {
        Statics.masterMind.powerUpDuration = float.Parse(newPowerUpDuration);
        Statics.masterMind.resetStaticStuff();
    }

    public void setMinPoints(string newMinPoints)
    {
        Statics.masterMind.pointValue = int.Parse(newMinPoints);
        Statics.masterMind.resetStaticStuff();
    }

    public void setPointFactor(string newPointFactor)
    {
        Statics.masterMind.pointFactor = float.Parse(newPointFactor);
        Statics.masterMind.resetStaticStuff();
    }

    public void setSpawnFactor(string newSpawnFactor)
    {
        Statics.masterMind.spawnFactor = float.Parse(newSpawnFactor);
        Statics.masterMind.resetStaticStuff();
    }

    public void setSpawnChance(string newSpawnChance)
    {
        Statics.masterMind.spawnChance = int.Parse(newSpawnChance);
        Statics.masterMind.resetStaticStuff();
    }

    public void setStartX(string newStartX)
    {
        Statics.masterMind.startX = int.Parse(newStartX);
        Statics.masterMind.resetStaticStuff();
    }

    public void setMinXInc(string newMinXInc)
    {
        Statics.masterMind.minXInc = int.Parse(newMinXInc);
        Statics.masterMind.resetStaticStuff();
    }

    public void setDefPUC(string newDefPUC)
    {
        Statics.masterMind.defPUC = float.Parse(newDefPUC);
        Statics.masterMind.resetStaticStuff();
    }

    public void setDefPC(string newDefPC)
    {
        Statics.masterMind.defPC = float.Parse(newDefPC);
        Statics.masterMind.resetStaticStuff();
    }

    public void setEnemySpeed(string newEnemySpeed)
    {
        Statics.masterMind.enemySpeed = float.Parse(newEnemySpeed);
        Statics.masterMind.resetStaticStuff();
    }

    public void setShouldTarget(bool newShouldTarget)
    {
        Statics.masterMind.shouldTarget = newShouldTarget;
        Statics.masterMind.resetStaticStuff();
    }

    public void menuFromGame()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Winner"))
        {
            Destroy(obj);
        }
        Player.transform.position = new Vector3(0, 0, 0);
        Statics.masterMind.gameState = 0;
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
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Winner"))
        {
            Destroy(obj);
        }
        Player.transform.position = new Vector3(0, 0, 0);
        Statics.masterMind.gameState = 0;
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
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Winner"))
        {
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(obj);
        }
        Player.transform.position = new Vector3(0, 0, 0);
        PlayingStuff.SetActive(true);
        GameOverStuff.SetActive(false);
        Statics.masterMind.gameState = 1;
        resetStuff();

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
