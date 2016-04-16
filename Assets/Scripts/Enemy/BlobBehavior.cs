﻿using UnityEngine;using System.Collections;using System;public class BlobBehavior : MonoBehaviour {	public float speed = 600f;    public float playerDistance = 5f;    public float playerFrightVariation = 25f;    public float maxVelocity = 25f;	private Vector3 moveDirection = Vector3.zero;	private Rigidbody2D rigidBody;    private GameObject player;	private float minX;	private float maxX;	private float minY;	private float maxY;	void Start()	{		// Get a reference to the rigidbody attached to the player		rigidBody = GetComponent<Rigidbody2D>();        player = GameObject.Find("Player");		// Set values for constraining movement within the camera view		float xHalfDistance = Camera.main.orthographicSize * Camera.main.aspect;		float yHalfDistance = Camera.main.orthographicSize;		minX = Camera.main.transform.position.x - xHalfDistance + 0.5f;		maxX = Camera.main.transform.position.x + xHalfDistance - 0.5f;		minY = Camera.main.transform.position.y - yHalfDistance + 0.4f;		maxY = Camera.main.transform.position.y + yHalfDistance - 0.5f;	}	// Update is called once per frame	void Update () {	}    private void constrainMovement()
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
    }    public void Attack()    {        if (rigidBody.velocity.magnitude > maxVelocity)
        {
            moveDirection = -0.5f * rigidBody.velocity;
        } else
        {
            moveDirection = -gameObject.transform.position.normalized;        }        Vector2 playerVector = rigidBody.position - player.GetComponent<Rigidbody2D>().position;        if (Math.Abs(playerVector.magnitude) <= playerDistance)        {            moveDirection += new Vector3(UnityEngine.Random.Range(0f, playerFrightVariation), UnityEngine.Random.Range(0f, playerFrightVariation), 0);        }		moveDirection *= speed;		// Move Rigidbody		rigidBody.AddRelativeForce(moveDirection * Time.deltaTime);        constrainMovement();    }    public void Flee()    {        if (rigidBody.velocity.magnitude > maxVelocity)        {            moveDirection = -0.5f * rigidBody.velocity;        }
        else        {            float closestXEdge = Math.Abs(rigidBody.position.x - minX) < Math.Abs(rigidBody.position.x - maxX) ? minX : maxX;            float closestYEdge = Math.Abs(rigidBody.position.y - minY) < Math.Abs(rigidBody.position.y - maxY) ? minY : maxY;            moveDirection = rigidBody.position - new Vector2(closestXEdge, closestYEdge);            moveDirection.Normalize();
        }        Vector2 playerVector = rigidBody.position - player.GetComponent<Rigidbody2D>().position;        if (Math.Abs(playerVector.magnitude) <= playerDistance)
        {
            moveDirection += new Vector3(UnityEngine.Random.Range(0f, playerFrightVariation), UnityEngine.Random.Range(0f, playerFrightVariation), 0);        }        moveDirection *= speed;		rigidBody.AddRelativeForce(moveDirection * Time.deltaTime);        constrainMovement();    }}