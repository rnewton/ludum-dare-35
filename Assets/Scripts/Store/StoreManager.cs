using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
	public int Coins { get; private set; }
	public bool Purchased { get; private set; }

	public Text UICoinCount;

	public void AddCoin()
	{
		Coins++;
	}

	public void SpendCoins(int howMany)
	{
		if (howMany > Coins) {
			Purchased = false;
			return;
		}

		Coins -= howMany;
		Purchased = true;
	}

	void Update()
	{
		UICoinCount.text = Coins.ToString ();
	}
}
