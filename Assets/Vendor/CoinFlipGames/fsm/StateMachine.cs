using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoinFlipGames.FSM
{
	public class StateMachine : MonoBehaviour
	{
		private class State
		{
			public Enum Name { get; private set; }
			public Action<Enum> OnEnter { get; private set; }
			public Action OnUpdate { get; private set; }
			public Action<Enum> OnExit { get; private set; }
			
			public State (Enum name, Action<Enum> enterCallback, Action updateCallback, Action<Enum> exitCallback)
			{
				this.Name = name;
				this.OnEnter = enterCallback;
				this.OnUpdate = updateCallback;
				this.OnExit = exitCallback;
			}
		}

		private Dictionary<Enum, State> states;
		private State currentState;
		private State previousState;

		public Enum CurrentState { get { return currentState.Name; } }
		public Enum PreviousState { get { return previousState.Name; } }

		public void AddState (Enum name, Action<Enum> enterCallback, Action updateCallback, Action<Enum> exitCallback)
		{
			State state = new State (name, enterCallback, updateCallback, exitCallback);
			this.states.Add (state.Name, state);

			if (null == this.currentState) {
				this.currentState = state;
				state.OnEnter (null);
			}
		}

		public void Switch (Enum to)
		{
			if (!this.states.ContainsKey (to)) {
				Debug.LogError ("No state by the name '" + to + "'.");
				return;
			}

			// Switch current to previous and pull new state by name
			this.previousState = this.currentState;
			this.currentState = this.states [to];

			// Leave previous state
			this.previousState.OnExit (this.CurrentState);

			// Enter new state
			this.currentState.OnEnter (this.PreviousState);
		}

		void Awake ()
		{
			this.states = new Dictionary<Enum, State> ();
		}

		void Update ()
		{
			if (null != this.currentState) {
				this.currentState.OnUpdate ();
			}
		}
	}
}
