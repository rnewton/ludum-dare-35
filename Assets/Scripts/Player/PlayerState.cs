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
	private StoreManager store;

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

		// Find the game UI and bind on the purchase events
		store = GameObject.Find ("GameUI").GetComponent<StoreManager> ();

		var buyTriangleButton = GameObject.Find("BuyTriangle").GetComponent<Button> ();
		buyTriangleButton.onClick.AddListener(SwitchToTriangle);

		var buySquareButton = GameObject.Find("BuySquare").GetComponent<Button> ();
		buySquareButton.onClick.AddListener(SwitchToSquare);

		var buyHexagonButton = GameObject.Find("BuyHexagon").GetComponent<Button> ();
		buyHexagonButton.onClick.AddListener(SwitchToHexagon);
	}

	private void SwitchToTriangle()
	{
		if (store.Purchased) {
			ShapeState.Switch (Shape.Triangle);
		}
	}

	private void SwitchToSquare()
	{
		if (store.Purchased) {
			ShapeState.Switch (Shape.Square);
		}
	}

	private void SwitchToHexagon()
	{
		if (store.Purchased) {
			ShapeState.Switch (Shape.Hexagon);
		}
	}
}
