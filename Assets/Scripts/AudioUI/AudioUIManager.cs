using UnityEngine;
using UnityEngine.UI;

public class AudioUIManager : MonoBehaviour
{
	public GameObject MuteButton;
	public GameObject UnmuteButton;

	private AudioSource musicManager;
	private AudioSource soundEffectManager;

	private bool muted = false;

	void Start ()
	{
		musicManager = GameObject.Find ("MusicManager").GetComponent<AudioSource> ();
		soundEffectManager = GameObject.Find ("SoundEffectManager").GetComponent<AudioSource> ();

		muted = 1 == PlayerPrefs.GetInt ("Muted", 0);
		if (muted) {
			Mute ();
		} else {
			Unmute ();
		}
	}

	public void Mute()
	{
		PlayerPrefs.SetInt ("Muted", 1);
		musicManager.volume = 0;
		soundEffectManager.volume = 0;

		MuteButton.SetActive (false);
		UnmuteButton.SetActive (true);
	}

	public void Unmute()
	{
		PlayerPrefs.SetInt ("Muted", 0);
		musicManager.volume = 1;
		soundEffectManager.volume = 1;

		MuteButton.SetActive (true);
		UnmuteButton.SetActive (false);
	}
}
