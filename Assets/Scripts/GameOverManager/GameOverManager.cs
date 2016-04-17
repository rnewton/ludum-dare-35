using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {
    void Start()
    {
        GameObject statManager = GameObject.Find("Stat Tracker");
        GameObject statsText = GameObject.Find("Stats");
        GameStats stats = statManager.GetComponent<GameStats>();
        statsText.GetComponent<Text>().text = "Blobs killed: " + stats.blobsKilled + "\nRounds survived: " + stats.roundsSurvived + "\nCoins at death: " + stats.currentCoins + "\nTotal coins earned: " + stats.totalCoinsEarned;

        Destroy(statManager);
    }

    void Update()
    {
    }

    public void restart()
    {
		SceneManager.LoadScene ("Game");
    }

    public void mainMenu()
    {
		SceneManager.LoadScene ("MainMenu");
    }
}
