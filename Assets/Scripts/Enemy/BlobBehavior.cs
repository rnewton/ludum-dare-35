﻿﻿using UnityEngine;using System.Collections;using System;public class BlobBehavior : MonoBehaviour {	public float speed = 600f;    public float playerDistance = 5f;    public float playerFrightMin = 10f;    public float playerFrightMax = 25f;    public float maxVelocity = 25f;    public GameObject coinPrefab;    public GameObject blobParticlesPrefab;	private Vector3 moveDirection = Vector3.zero;	private Rigidbody2D rigidBody;    private GameObject player;    private GameObject dodecagram;	private float minX;	private float maxX;	private float minY;	private float maxY;	private SoundEffectManager soundEffectManager;	void Start()	{		// Get a reference to the rigidbody attached to the player		rigidBody = GetComponent<Rigidbody2D>();        player = GameObject.Find("Player");        dodecagram = GameObject.Find("Dodecagram");		// Set values for constraining movement within the camera view		float xHalfDistance = Camera.main.orthographicSize * Camera.main.aspect;		float yHalfDistance = Camera.main.orthographicSize;		minX = Camera.main.transform.position.x - xHalfDistance + 0.5f;		maxX = Camera.main.transform.position.x + xHalfDistance - 0.5f;		minY = Camera.main.transform.position.y - yHalfDistance + 0.4f;		maxY = Camera.main.transform.position.y + yHalfDistance - 0.5f;

        soundEffectManager = GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>();
    }    private void constrainMovement()
    {
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
    }    private void dampen()
    {
        float velocityMag = Math.Abs(rigidBody.velocity.magnitude);

        // stop entirely if we're going caraaaaaazy
        if (velocityMag > maxVelocity)
        {
            rigidBody.velocity = (-1f * (velocityMag / maxVelocity) * rigidBody.velocity * rigidBody.mass);
        }
    }    public void Attack()    {        moveDirection = -gameObject.transform.position.normalized;        moveDirection += getPlayerAvoidance();		moveDirection *= speed;		// Move Rigidbody		rigidBody.AddRelativeForce(moveDirection * Time.deltaTime);        constrainMovement();        dampen();    }    public void Flee()    {        float closestXEdge = Math.Abs(rigidBody.position.x - minX) < Math.Abs(rigidBody.position.x - maxX) ? minX : maxX;        float closestYEdge = Math.Abs(rigidBody.position.y - minY) < Math.Abs(rigidBody.position.y - maxY) ? minY : maxY;        moveDirection = rigidBody.position - new Vector2(closestXEdge, closestYEdge);        moveDirection.Normalize();

        moveDirection += getPlayerAvoidance();
        moveDirection *= speed;		rigidBody.AddRelativeForce(moveDirection * Time.deltaTime);        constrainMovement();        dampen();    }    private Vector3 getPlayerAvoidance()
    {
        Vector2 playerVector = rigidBody.position - player.GetComponent<Rigidbody2D>().position;
        float playerVecMagnitude = Math.Abs(playerVector.magnitude);
        Vector3 avoidanceVector = new Vector3(0, 0, 0);
        if (playerVecMagnitude <= playerDistance)
        {
            //float repulsion = playerVecMagnitude / playerDistance;
            float repulsion = 1f;

            avoidanceVector += new Vector3(
                repulsion * (playerVector.x / playerVecMagnitude) * UnityEngine.Random.Range(playerFrightMin, playerFrightMax), 
                repulsion * (playerVector.y / playerVecMagnitude) * UnityEngine.Random.Range(playerFrightMin, playerFrightMax), 
                0
           );
        }

        return avoidanceVector;
    }    public void die()
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
            );			// Remove the coin after 15 seconds if it doesn't get picked up			Destroy (coin, 15f);
        }

        DodecagramPiece piece = gameObject.GetComponentInChildren<DodecagramPiece>();        if (piece != null)
        {
            if (piece.gameObject.transform.position.x <= minX || piece.gameObject.transform.position.x >= maxX ||
                piece.gameObject.transform.position.y <= minY || piece.gameObject.transform.position.y >= maxY)
            {
                piece.gameObject.transform.SetParent(dodecagram.transform);
                piece.goHome();
            } else
            {
                piece.gameObject.transform.SetParent(dodecagram.transform);
                piece.state = DodecagramPiece.states.Dropped;
            }
        }		// Effects		GameObject particles = (GameObject)Instantiate (blobParticlesPrefab, transform.position, Quaternion.identity);		Destroy (particles, 1f);		soundEffectManager.PlayClip ("splat" + UnityEngine.Random.Range (1, 4));    }}