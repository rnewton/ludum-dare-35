using CoinFlipGames.FSM;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
	public GameObject triangleShape;
	public GameObject squareShape;
	public GameObject hexagonShape;

	public GameObject ShapeChangePrefab;

	public StateMachine ShapeState;
	public enum Shape
	{
		Triangle,
		Square,
		Hexagon,
	}

	private MovePlayer movementScript;

	// Use this for initialization
	void Start ()
	{
		movementScript = GetComponent<MovePlayer> ();

		// Set the attack based on the state
		ShapeState.AddState(
			Shape.Triangle,
			(previous) => { 
				triangleShape.SetActive(true);
				SpawnShapeChangeParticles ();
			},
			movementScript.TriangleUpdate,
			(next) => {
				triangleShape.SetActive(false);
			}
		);

		ShapeState.AddState(
			Shape.Square,
			(previous) => {
				squareShape.SetActive(true);
				SpawnShapeChangeParticles ();
			},
			movementScript.SquareUpdate,
			(next) => {
				squareShape.SetActive(false);
			}
		);

		ShapeState.AddState(
			Shape.Hexagon,
			(previous) => {
				hexagonShape.SetActive(true);
				SpawnShapeChangeParticles ();
			},
			movementScript.HexagonUpdate,
			(next) => {
				hexagonShape.SetActive(false);
			}
		);
	}

	public void SwitchToTriangle()
	{
		ShapeState.Switch (Shape.Triangle);
	}

	public void SwitchToSquare()
	{
		ShapeState.Switch (Shape.Square);
	}

	public void SwitchToHexagon()
	{
		ShapeState.Switch (Shape.Hexagon);
	}

	public void SpawnShapeChangeParticles()
	{
		var particles = Instantiate (ShapeChangePrefab, transform.position, Quaternion.identity);
		Destroy (particles, 1f);
	}

	public string CurrentShape()
	{
		return ShapeState.CurrentState.ToString ();
	}
}
