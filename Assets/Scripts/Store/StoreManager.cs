using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public int Coins { get; private set; }
	public bool Purchased { get; private set; }

	public Text UICoinCount;
	public Animator StoreAnimator;

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

	public void OnPointerEnter(PointerEventData eventData)
	{
		StoreAnimator.SetBool ("Hover", true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		StoreAnimator.SetBool ("Hover", false);
	}

	void Update()
	{
		UICoinCount.text = Coins.ToString ();
	}
}
