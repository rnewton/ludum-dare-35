# FSM
## Simple finite state machine for Unity

### Usage

```csharp
using CoinFlipGames.FSM;
using System;
using UnityEngine;

public class MyMonoBehaviour : MonoBehaviour
{
	// Set in the inspector!
	public StateMachine stateMachine;

	private enum States
	{
		Start,
		Player1Turn,
		Player2Turn,
		Player1Win,
		Player2Win
	}

	void Start ()
	{
		// First state becomes the default and runs automatically
		stateMachine.AddState(
			States.Start,
			(previous) => { Debug.Log ("Start.OnEnter"); }, // Can use lambda functions
			() => { Debug.Log ("Start.OnUpdate"); },
			(next) => { Debug.Log ("Start.OnExit"); }
		);

		stateMachine.AddState(
			States.Player1Turn,
			this.StartPlayer1Turn, // Or plain functions as long as they match Action<Enum>
			...
		);
	}

	private void StartPlayer1Turn (Enum previous)
	{
		Debug.Log ("Player1Turn.OnEnter");
	}
}
```
