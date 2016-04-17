using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
	private StoreManager store;
	private SoundEffectManager soundEffectManager;

	void Start ()
	{
		store = GameObject.Find ("GameUI").GetComponent<StoreManager> ();
		soundEffectManager = GameObject.Find ("SoundEffectManager").GetComponent<SoundEffectManager> ();
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.CompareTag ("Coin")) {
			store.AddCoin ();
			soundEffectManager.PlayClip ("coin");
			Destroy (c.gameObject);
		}
	}
}
