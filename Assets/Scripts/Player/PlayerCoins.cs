using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
	private StoreManager store;

	void Start ()
	{
		store = GameObject.Find ("GameUI").GetComponent<StoreManager> ();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.CompareTag ("Coin")) {
			store.AddCoin ();
			Destroy (c.gameObject);
		}
	}
}
