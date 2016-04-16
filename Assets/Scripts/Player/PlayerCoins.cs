using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
	public int Coins { get; private set; }

	public void AddCoin()
	{
		Coins++;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.CompareTag ("Coin")) {
			AddCoin ();
			Destroy (c.gameObject);
		}
	}
}
