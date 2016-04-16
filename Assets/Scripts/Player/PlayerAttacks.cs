using UnityEngine;
using System.Collections;

public class PlayerAttacks : MonoBehaviour
{
	private MovePlayer movement;

	void Start ()
	{
		movement = GetComponent<MovePlayer> ();
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.CompareTag ("Enemy") && movement.Attacking) {
			Destroy (c.gameObject);
		}
	}
}
