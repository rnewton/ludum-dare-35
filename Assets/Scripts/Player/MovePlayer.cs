using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePlayer : MonoBehaviour
{
	public float speed = 600f;

	public float triangleAttackCooldown = 3f;
	public float squareAttackCooldown = 3f;
	public float hexagonAttackCooldown = 3f;

	private float attackTimer;

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
	}

	private void BaseUpdate() 
	{
		// Use input up and down for direction, multiplied by speed
		moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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

	public void TriangleUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			rigidBody.AddTorque (1000);
			attackTimer = triangleAttackCooldown;
		}

		attackTimer -= Time.deltaTime;
	}

	public void SquareUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			Debug.Log ("Square Attack");
			attackTimer = squareAttackCooldown;
		}

		attackTimer -= Time.deltaTime;
	}

	public void HexagonUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			Debug.Log ("Hexagon Attack");
			attackTimer = hexagonAttackCooldown;
		}

		attackTimer -= Time.deltaTime;
	}
}
