using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
	public float speed = 600f;

	public float triangleAttackCooldown = 3f;
	public float squareAttackCooldown = 3f;
	public float hexagonAttackCooldown = 3f;

	private float attackTimer;

	private Vector3 moveDirection = Vector3.zero;
	private Rigidbody rigidBody;

	private float minX;
	private float maxX;
	private float minZ;
	private float maxZ;

	void Start()
	{
		// Get a reference to the rigidbody attached to the player
		rigidBody = GetComponent<Rigidbody>();

		// Set values for constraining movement within the camera view
		float xHalfDistance = Camera.main.orthographicSize * Camera.main.aspect;
		float zHalfDistance = Camera.main.orthographicSize;

		minX = Camera.main.transform.position.x - xHalfDistance + 0.5f;
		maxX = Camera.main.transform.position.x + xHalfDistance - 0.5f;

		minZ = Camera.main.transform.position.z - zHalfDistance + 0.4f;
		maxZ = Camera.main.transform.position.z + zHalfDistance - 0.5f;
	}

	private void BaseUpdate() 
	{
		// Use input up and down for direction, multiplied by speed
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection *= speed;

		// Move Rigidbody
		rigidBody.AddRelativeForce(moveDirection * Time.deltaTime);

		// Constrain to camera viewport
		if (rigidBody.position.x < minX) {
			rigidBody.AddForce(Vector3.right * speed * Time.deltaTime);
		} else if (rigidBody.position.x > maxX) {
			rigidBody.AddForce(Vector3.left * speed * Time.deltaTime);
		}

		if (rigidBody.position.z < minZ) {
			rigidBody.AddForce(Vector3.forward * speed * Time.deltaTime);
		} else if (rigidBody.position.z > maxZ) {
			rigidBody.AddForce(Vector3.back * speed * Time.deltaTime);
		}

		// Rotate to face mouse
		Vector3 target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.LookAt (new Vector3 (target.x, 1, target.z));
	}

	public void TriangleUpdate()
	{
		BaseUpdate ();

		if (attackTimer <= 0 && Input.GetButtonUp ("Attack")) {
			Debug.Log ("Triangle Attack");
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
