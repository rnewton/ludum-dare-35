using System.Collections.Generic;
using UnityEngine;

namespace CoinFlipGames.PoolParty
{
	public class PoolParty
	{
		/// <summary>
		/// Data structure that holds inactive entities in the pool and
		/// provides an interface for spawning/despawning.
		/// </summary>
		public class Pool
		{
			/// <summary>
			/// The identifier for instantiated entities.
			/// </summary>
			private int identifier = 1;
			
			/// <summary>
			/// Inactive entities in the pool.
			/// </summary>
			Stack<GameObject> inactive;
			
			/// <summary>
			/// Prefab instantiated for the pool.
			/// </summary>
			GameObject prefab;

			/// <summary>
			/// Initializes a new Pool for the given prefab.
			/// </summary>
			public Pool (GameObject prefab)
			{
				this.prefab = prefab;
				this.inactive = new Stack<GameObject> ();
			}
			
			/// <summary>
			/// Spawn the prefab at the specified position and rotation.
			/// If the inactive pool is empty, the prefab will be instantiated fresh.
			/// If the inactive pool contains an object already, we can reset it and use it
			/// as if it were instantiated fresh.
			/// </summary>
			public GameObject Instantiate (Vector3 position, Quaternion rotation)
			{
				GameObject entity;
				if (0 == this.inactive.Count) {
					// We don't have an object in our pool. Instantiate a fresh one
					entity = (GameObject)GameObject.Instantiate (this.prefab, position, rotation);
					entity.name = this.prefab.name + " (" + (this.identifier++) + ")";
					
					// PoolMember component allows us to have a reference back to this pool from the object.
					entity.AddComponent<PoolMember> ().Pool = this;
				} else {
					// Grab the last object in the inactive array
					entity = this.inactive.Pop();

					// If the entity doesn't exist anymore (destroyed external to this script)
					if (entity == null) {
						// Try again from the start
						return this.Instantiate (position, rotation);
					}
				}
				
				entity.transform.position = position;
				entity.transform.rotation = rotation;

				// Reset values on the entity automatically
				PooledEntity pooledEntityScript = entity.GetComponent<PooledEntity> ();
				if (pooledEntityScript != null) {
					pooledEntityScript.ResetEntity ();
				}

				entity.SetActive(true);

				return entity;
			}
			
			/// <summary>
			/// Move the specified entity to the inactive pool.
			/// </summary>
			public void Destroy (GameObject entity)
			{
				entity.SetActive (false);
				this.inactive.Push (entity);
			}
		}

		/// <summary>
		/// Added to freshly instantiated objects, so we can link back
		/// to the correct pool on despawn.
		/// </summary>
		public class PoolMember : MonoBehaviour
		{
			/// <summary>
			/// Reference to holding pool for this entity
			/// </summary>
			public Pool Pool { get; set; }

			/// <summary>
			/// Allows the entity to be destroyed after "time" seconds
			/// </summary>
			public void Destroy (float time = 0)
			{
				Invoke ("DestroyFromPool", time);
			}

			/// <summary>
			/// Destroys this entity from the holding pool.
			/// </summary>
			private void DestroyFromPool ()
			{
				this.Pool.Destroy (this.gameObject);
			}
		}
			
		/// <summary>
		/// Collection of all pools by prefab.
		/// </summary>
		private static Dictionary<GameObject, Pool> pools;



		/// <summary>
		/// Creates a new pool for the given prefab if it doesn't already exist.
		/// Does nothing if the pool for the prefab already exists.
		/// </summary>
		private static void CreatePool (GameObject prefab)
		{
			if (pools == null) {
				pools = new Dictionary<GameObject, Pool> ();
			}

			if (pools.ContainsKey(prefab) == false) {
				pools [prefab] = new Pool (prefab);
			}
		}
		
		/// <summary>
		/// Preload the specified prefab and amount.
		/// </summary>
		public static void Preload (GameObject prefab, int amount)
		{
			CreatePool (prefab);

			// Create the specified number of entities and immediately
			// despawn them to place in the inactive stack
			GameObject[] entities = new GameObject [amount];
			for (int i = 0; i < amount; i++) {
				entities [i] = Instantiate (prefab, Vector3.zero, Quaternion.identity);
			}

			for (int i = 0; i < amount; i++) {
				Destroy (entities [i]);
			}
		}
		
		/// <summary>
		/// Spawn the prefab at the specified position and rotation.
		/// </summary>
		public static GameObject Instantiate (GameObject prefab, Vector3 position, Quaternion rotation)
		{
			CreatePool (prefab);
			return pools [prefab].Instantiate (position, rotation);
		}

		/// <summary>
		/// Spawn the prefab at the default position and rotation.
		/// </summary>
		public static GameObject Instantiate (GameObject prefab)
		{
			CreatePool (prefab);
			return pools [prefab].Instantiate (Vector3.zero, Quaternion.identity);
		}

		/// <summary>
		/// Despawn the specified prefab.
		/// </summary>
		public static void Destroy (GameObject entity, float time = 0)
		{
			PoolMember pm = entity.GetComponent<PoolMember> ();
			if (null == pm) {
				Debug.LogError ("GameObject '" + entity.name + "' wasn't spawned from a pool.");
			} else {
				pm.Destroy (time);
			}
		}
	}
}
