using UnityEngine;
using System.Collections;

public class PlayerAttacks : MonoBehaviour
{
	private MovePlayer movement;
	private CameraShake cameraShake;

	void Start ()
	{
		movement = GetComponent<MovePlayer> ();
		cameraShake = GetComponent<CameraShake> ();
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.CompareTag ("Enemy") && movement.Attacking) {
			cameraShake.Shake (0.5f, 0.1f, 1f);
			Destroy (c.gameObject);
		}
	}
}
