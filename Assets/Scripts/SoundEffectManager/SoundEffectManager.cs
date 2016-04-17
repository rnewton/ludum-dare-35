using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
	public AudioClip[] clips;

	private Dictionary<string, AudioClip> namedClips;
	private AudioSource source;

	void Start()
	{
		namedClips = new Dictionary<string, AudioClip> ();
		foreach (AudioClip clip in clips) {
			namedClips.Add (clip.name, clip);
		}

		source = GetComponent<AudioSource> ();
	}

	public void PlayClip(string name)
	{
		if (namedClips.ContainsKey (name)) {
			source.PlayOneShot (namedClips [name]);
		} else {
			Debug.LogError ("Tried to play clip '" + name + "', but I don't have anything named that");
		}
	}
}
