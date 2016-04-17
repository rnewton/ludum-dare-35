using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
	public Text WaveCountLabel;
	public Text BlobCountLabel;

	public GameObject WaveContainer;
	public GameObject TimerContainer;

	private GameManager gameManager;
	private PlayerAttacks playerAttacks;

	void Start ()
	{
		gameManager = GameObject.Find ("Environment").GetComponent<GameManager> ();
		playerAttacks = GameObject.Find ("Player").GetComponent<PlayerAttacks> ();
	}

	void Update ()
	{
		if (gameManager.transitioning) {
			WaveContainer.SetActive (false);
			TimerContainer.SetActive (true);
		} else {
			WaveContainer.SetActive (true);
			TimerContainer.SetActive (false);

			WaveCountLabel.text = "Wave %d".Replace ("%d", gameManager.WaveCount ().ToString ());

			int totalBlobs = gameManager.BlobsThisWave ();

			BlobCountLabel.text = "%1$d/%2$d"
				.Replace ("%1$d", (totalBlobs - playerAttacks.WaveKillCount).ToString ())
				.Replace ("%2$d", totalBlobs.ToString ());
		}
	}
}
