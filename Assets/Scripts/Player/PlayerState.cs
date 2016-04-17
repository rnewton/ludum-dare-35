using CoinFlipGames.FSM;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
	public GameObject triangleShape;
	public GameObject squareShape;
	public GameObject hexagonShape;

	public StateMachine ShapeState;
	private enum Shape
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
				Debug.Log ("∆"); 
				triangleShape.SetActive(true);
			},
			movementScript.TriangleUpdate,
			(next) => {
				triangleShape.SetActive(false);
			}
		);

		ShapeState.AddState(
			Shape.Square,
			(previous) => {
				Debug.Log ("□");
				squareShape.SetActive(true);
			},
			movementScript.SquareUpdate,
			(next) => {
				squareShape.SetActive(false);
			}
		);

		ShapeState.AddState(
			Shape.Hexagon,
			(previous) => {
				Debug.Log ('\u2B21'); 
				hexagonShape.SetActive(true);
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
}
