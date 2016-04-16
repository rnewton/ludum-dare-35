using UnityEngine;
using System.Collections;

public class BlobBehavior : MonoBehaviour {
	public float speed = 600f;

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


	// Update is called once per frame
	void Update () {

	}

    private void BaseUpdate()
    {
        // Use input up and down for direction, multiplied by speed
        moveDirection = getMoveDir();
		moveDirection *= speed;

		// Move Rigidbody
		rigidBody.AddRelativeForce(moveDirection * Time.deltaTime);

		// Constrain to camera viewport
		float distance = Vector2.Distance (transform.position, Vector2.zero);

		if (rigidBody.position.x < minX) {
			rigidBody.AddForce(Vector2.right * speed * distance * Time.deltaTime);
		} else if (rigidBody.position.x > maxX) {
			rigidBody.AddForce(Vector2.left * speed * distance * Time.deltaTime);
		}

		if (rigidBody.position.y < minY) {
			rigidBody.AddForce(Vector2.up * speed * distance * Time.deltaTime);
		} else if (rigidBody.position.y > maxY) {
			rigidBody.AddForce(Vector2.down * speed * distance * Time.deltaTime);
		}
    }

    private Vector2 getMoveDir()
    {
        return -gameObject.transform.position.normalized;
    }

    public void Attack()
    {
        BaseUpdate();
    }
}
