using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour {
    public void restart()
    {
		SceneManager.LoadScene ("Game");
    }

    public void mainMenu()
    {
		SceneManager.LoadScene ("MainMenu");
    }
}
