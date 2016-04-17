using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePlayer : MonoBehaviour
{
	public float Speed = 20f;
	public float TriangleTorque = 1000f;
	public float SquareSpeed = 40f;
	public Animator HexagonAnimator;

	public bool Attacking { get; private set; }

	public float TriangleAttackCooldown = 3f;
	public float SquareAttackCooldown = 3f;
	public float HexagonAttackCooldown = 3f;

	public TrailRenderer Trail;
	public Color NormalTrailColor;
	public Color AttackTrailColor;

	private float originalSpeed;

	private float attackTimer;

	private Vector3 moveDirection = Vector3.zero;
	private Rigidbody2D rigidBody;

	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	private SoundEffectManager soundEffectManager;

	void Start()
	{
		// Get a reference to the rigidbody attached to the player
		rigidBody = GetComponent<Rigidbody2D>();
		soundEffectManager = GameObject.Find ("SoundEffectManager").GetComponent<SoundEffectManager> ();

		// Set values for constraining movement within the camera view
		float xHalfDistance = Camera.main.orthographicSize * Camera.main.aspect;
		float yHalfDistance = Camera.main.orthographicSize;

		minX = Camera.main.transform.position.x - xHalfDistance + 0.5f;
		maxX = Camera.main.transform.position.x + xHalfDistance - 0.5f;

		minY = Camera.main.transform.position.y - yHalfDistance + 0.4f;
		maxY = Camera.main.transform.position.y + yHalfDistance - 0.5f;

		// Set defaults
		originalSpeed = Speed;
		NormalTrail ();
	}

	private void BaseUpdate() 
	{
		// Use input up and down for direction, multiplied by speed
		moveDirection = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		moveDirection *= Speed * Time.deltaTime;
		transform.Translate (moveDirection, Space.World);

		// Constrain to camera viewport
		float distance = Vector2.Distance (transform.position, Vector2.zero);

		if (rigidBody.position.x < minX) {
			rigidBody.AddForce(Vector2.right * Speed * distance);
		} else if (rigidBody.position.x > maxX) {
			rigidBody.AddForce(Vector2.left * Speed * distance);
		}

		if (rigidBody.position.y < minY) {
			rigidBody.AddForce(Vector2.up * Speed * distance);
		} else if (rigidBody.position.y > maxY) {
			rigidBody.AddForce(Vector2.down * Speed * distance);
		}
	}

	private void NormalTrail ()
	{
		Trail.material.SetColor ("_Color", NormalTrailColor);
	}

	private void AttackTrail ()
	{
		Trail.material.SetColor ("_Color", AttackTrailColor);
	}

	public void TriangleUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			Attacking = true;
			AttackTrail ();
			rigidBody.AddTorque (TriangleTorque);
			attackTimer = TriangleAttackCooldown;

			soundEffectManager.PlayClip ("triangleAttack");
			Invoke ("ResetAfterTriangleAttack", TriangleAttackCooldown);
		}

		attackTimer -= Time.deltaTime;
	}

	private void ResetAfterTriangleAttack()
	{
		Attacking = false;
		NormalTrail ();
	}

	public void SquareUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			Attacking = true;
			AttackTrail ();
			Speed = SquareSpeed;
			attackTimer = SquareAttackCooldown;

			soundEffectManager.PlayClip ("squareAttack");
			Invoke ("ResetAfterSquareAttack", SquareAttackCooldown);
		}

		attackTimer -= Time.deltaTime;
	}

	private void ResetAfterSquareAttack()
	{
		Speed = originalSpeed;
		Attacking = false;
		NormalTrail ();
	}

	public void HexagonUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			Attacking = true;
			AttackTrail ();
			HexagonAnimator.SetBool ("attacking", true);
			attackTimer = HexagonAttackCooldown;

			soundEffectManager.PlayClip ("hexagonAttack");
			Invoke ("ResetAfterHexagonAttack", HexagonAttackCooldown);
		}

		attackTimer -= Time.deltaTime;
	}

	private void ResetAfterHexagonAttack()
	{
		HexagonAnimator.SetBool ("attacking", false);
		Attacking = false;
		NormalTrail ();
	}
}
