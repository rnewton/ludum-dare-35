using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
	public Text TimerLabel;

	private GameManager gameManager;

	void Start ()
	{
		gameManager = GameObject.Find ("Environment").GetComponent<GameManager> ();
	}

	void Update ()
	{
		TimerLabel.text = gameManager.TimeLeftInWave ().ToString ("F2");
	}
}
