using UnityEngine;using System.Collections;using System;public class BlobBehavior : MonoBehaviour {	public float speed = 600f;    public float playerDistance = 5f;    public float playerFrightVariation = 25f;    public float maxVelocity = 25f;    public GameObject coinPrefab;	public GameObject blobParticlesPrefab;	private Vector3 moveDirection = Vector3.zero;	private Rigidbody2D rigidBody;    private GameObject player;    private GameObject dodecagram;	private float minX;	private float maxX;	private float minY;	private float maxY;	void Start()	{		// Get a reference to the rigidbody attached to the player		rigidBody = GetComponent<Rigidbody2D>();        player = GameObject.Find("Player");        dodecagram = GameObject.Find("Dodecagram");		// Set values for constraining movement within the camera view		float xHalfDistance = Camera.main.orthographicSize * Camera.main.aspect;		float yHalfDistance = Camera.main.orthographicSize;		minX = Camera.main.transform.position.x - xHalfDistance + 0.5f;		maxX = Camera.main.transform.position.x + xHalfDistance - 0.5f;		minY = Camera.main.transform.position.y - yHalfDistance + 0.4f;		maxY = Camera.main.transform.position.y + yHalfDistance - 0.5f;	}	// Update is called once per frame	void Update () {	}    private void constrainMovement()
    {
		// Constrain to camera viewport
		float distance = Vector2.Distance (transform.position, Vector2.zero);

		if (rigidBody.position.x < minX) {
			rigidBody.AddForce(Vector2.right * speed * distance * Time.deltaTime * rigidBody.mass);
		} else if (rigidBody.position.x > maxX) {
			rigidBody.AddForce(Vector2.left * speed * distance * Time.deltaTime * rigidBody.mass);
		}

		if (rigidBody.position.y < minY) {
			rigidBody.AddForce(Vector2.up * speed * distance * Time.deltaTime * rigidBody.mass);
		} else if (rigidBody.position.y > maxY) {
			rigidBody.AddForce(Vector2.down * speed * distance * Time.deltaTime * rigidBody.mass);
		}
    }    private void dampen()
    {
        float velocityMag = Math.Abs(rigidBody.velocity.magnitude);

        // stop entirely if we're going caraaaaaazy
        if (velocityMag > 2f * maxVelocity)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = 0f;
        } else if (velocityMag > maxVelocity)
        {
            rigidBody.AddRelativeForce(-1.0f * (1f - (velocityMag / maxVelocity)) * rigidBody.velocity * rigidBody.mass);
        }
    }    public void Attack()    {        dampen();        moveDirection = -gameObject.transform.position.normalized;        Vector2 playerVector = rigidBody.position - player.GetComponent<Rigidbody2D>().position;        if (Math.Abs(playerVector.magnitude) <= playerDistance)        {            moveDirection += new Vector3(UnityEngine.Random.Range(0f, playerFrightVariation), UnityEngine.Random.Range(0f, playerFrightVariation), 0);        }		moveDirection *= speed;		// Move Rigidbody		rigidBody.AddRelativeForce(moveDirection * Time.deltaTime);        constrainMovement();    }    public void Flee()    {        dampen();        float closestXEdge = Math.Abs(rigidBody.position.x - minX) < Math.Abs(rigidBody.position.x - maxX) ? minX : maxX;        float closestYEdge = Math.Abs(rigidBody.position.y - minY) < Math.Abs(rigidBody.position.y - maxY) ? minY : maxY;        moveDirection = rigidBody.position - new Vector2(closestXEdge, closestYEdge);        moveDirection.Normalize();
        Vector2 playerVector = rigidBody.position - player.GetComponent<Rigidbody2D>().position;        float playerVecMagnitude = Math.Abs(playerVector.magnitude);        if (playerVecMagnitude <= playerDistance)
        {
            moveDirection += new Vector3(                (-playerVector.x / playerVecMagnitude) * UnityEngine.Random.Range(-playerFrightVariation, playerFrightVariation),                 (-playerVector.y / playerVecMagnitude) * UnityEngine.Random.Range(-playerFrightVariation, playerFrightVariation),                 0           );        }        moveDirection *= speed;		rigidBody.AddRelativeForce(moveDirection * Time.deltaTime);        constrainMovement();    }    public void die()
    {
        for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
        {
            GameObject coin = (GameObject)Instantiate(coinPrefab, 
                new Vector3(
                    gameObject.transform.position.x + UnityEngine.Random.Range(-1f, 1f), 
                    gameObject.transform.position.y + UnityEngine.Random.Range(-1f, 1f),
                    -5
                ), 
                Quaternion.identity
            );
        }

        DodecagramPiece piece = gameObject.GetComponentInChildren<DodecagramPiece>();        if (piece != null)
        {
            piece.gameObject.transform.SetParent(dodecagram.transform);
            piece.state = DodecagramPiece.states.Dropped;
        }		GameObject particles = (GameObject)Instantiate (blobParticlesPrefab, transform.position, Quaternion.identity);		Destroy (particles, 1f);    }}