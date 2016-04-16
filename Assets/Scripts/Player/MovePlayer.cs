using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePlayer : MonoBehaviour
{
	public float speed = 20f;
	public float triangleTorque = 1000f;
	public float squareSpeed = 40f;
	public Animator hexagonAnimator;

	public float triangleAttackCooldown = 3f;
	public float squareAttackCooldown = 3f;
	public float hexagonAttackCooldown = 3f;

	public TrailRenderer trail;
	public Color normalTrailColor;
	public Color attackTrailColor;

	private float originalSpeed;

	private float attackTimer;
	private bool attacking;

	private Vector3 moveDirection = Vector3.zero;
	private Rigidbody2D rigidBody;

	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	void Start()
	{
		// Get a reference to the rigidbody attached to the player
		rigidBody = GetComponent<Rigidbody2D>();

		// Set values for constraining movement within the camera view
		float xHalfDistance = Camera.main.orthographicSize * Camera.main.aspect;
		float yHalfDistance = Camera.main.orthographicSize;

		minX = Camera.main.transform.position.x - xHalfDistance + 0.5f;
		maxX = Camera.main.transform.position.x + xHalfDistance - 0.5f;

		minY = Camera.main.transform.position.y - yHalfDistance + 0.4f;
		maxY = Camera.main.transform.position.y + yHalfDistance - 0.5f;

		// Set defaults
		originalSpeed = speed;
		NormalTrail ();
	}

	private void BaseUpdate() 
	{
		// Use input up and down for direction, multiplied by speed
		moveDirection = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		moveDirection *= speed * Time.deltaTime;
		transform.Translate (moveDirection, Space.World);

		// Constrain to camera viewport
		float distance = Vector2.Distance (transform.position, Vector2.zero);

		if (rigidBody.position.x < minX) {
			rigidBody.AddForce(Vector2.right * speed * distance);
		} else if (rigidBody.position.x > maxX) {
			rigidBody.AddForce(Vector2.left * speed * distance);
		}

		if (rigidBody.position.y < minY) {
			rigidBody.AddForce(Vector2.up * speed * distance);
		} else if (rigidBody.position.y > maxY) {
			rigidBody.AddForce(Vector2.down * speed * distance);
		}
	}

	private void NormalTrail ()
	{
		trail.material.SetColor ("_Color", normalTrailColor);
	}

	private void AttackTrail ()
	{
		trail.material.SetColor ("_Color", attackTrailColor);
	}

	public void TriangleUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			attacking = true;
			AttackTrail ();
			rigidBody.AddTorque (triangleTorque);
			attackTimer = triangleAttackCooldown;

			Invoke ("ResetAfterTriangleAttack", triangleAttackCooldown);
		}

		attackTimer -= Time.deltaTime;
	}

	private void ResetAfterTriangleAttack()
	{
		attacking = false;
		NormalTrail ();
	}

	public void SquareUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			attacking = true;
			AttackTrail ();
			speed = squareSpeed;
			attackTimer = squareAttackCooldown;

			Invoke ("ResetAfterSquareAttack", squareAttackCooldown);
		}

		attackTimer -= Time.deltaTime;
	}

	private void ResetAfterSquareAttack()
	{
		speed = originalSpeed;
		attacking = false;
		NormalTrail ();
	}

	public void HexagonUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			attacking = true;
			AttackTrail ();
			hexagonAnimator.SetBool ("attacking", true);
			attackTimer = hexagonAttackCooldown;

			Invoke ("ResetAfterHexagonAttack", hexagonAttackCooldown);
		}

		attackTimer -= Time.deltaTime;
		attacking = false;
	}

	private void ResetAfterHexagonAttack()
	{
		hexagonAnimator.SetBool ("attacking", false);
		attacking = false;
		NormalTrail ();
	}
}
