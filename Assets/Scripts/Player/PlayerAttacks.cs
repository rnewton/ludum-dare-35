using UnityEngine;
using System.Collections;

public class PlayerAttacks : MonoBehaviour
{
	public int TotalKillCount { get; private set; }
	public int WaveKillCount { get; private set; }

	private MovePlayer movement;
	private CameraShake cameraShake;
	private SoundEffectManager soundEffectManager;

	void Start ()
	{
		movement = GetComponent<MovePlayer> ();
		cameraShake = GetComponent<CameraShake> ();
		soundEffectManager = GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>();
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.CompareTag ("Enemy") && movement.Attacking) {
			cameraShake.Shake (0.5f, 0.1f, 1f);
            c.gameObject.GetComponent<BlobBehavior>().die();
			Destroy (c.gameObject);
			soundEffectManager.PlayClip ("splat" + UnityEngine.Random.Range (1, 4));

			TotalKillCount++;
			WaveKillCount++;
		}
	}

	public void NewWave() // Hairdos
	{
		WaveKillCount = 0;
	}
}
