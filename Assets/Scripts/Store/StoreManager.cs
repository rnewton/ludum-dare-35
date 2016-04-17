using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public int Coins { get; private set; }
	public bool Purchased { get; private set; }

	public Text UICoinCount;
	public Animator StoreAnimator;

	public Text TriangleBuyButtonLabel;
	public Text SquareBuyButtonLabel;
	public Text HexagonBuyButtonLabel;

	public int baseTrianglePrice = 5;
	public int baseSquarePrice = 10;
	public int baseHexagonPrice = 15;

	public float trianglePriceMultiplier = 1.5f;
	public float squarePriceMultiplier = 1.5f;
	public float hexagonPriceMultiplier = 1.5f;

	private int currentTrianglePrice;
	private int currentSquarePrice;
	private int currentHexagonPrice;

	public void AddCoin()
	{
		Coins++;
	}

	public void CashMoneys()
	{
		Coins += 1000;
	}

	public void SpendCoins(string which)
	{
		int howMany = int.MaxValue; // Bajillion
		switch (which) {
		case "triangle":
			howMany = currentTrianglePrice;
			break;
		case "square":
			howMany = currentSquarePrice;
			break;
		case "hexagon":
			howMany = currentHexagonPrice;
			break;
		}

		if (howMany > Coins) {
			Purchased = false;
			return;
		}

		Coins -= howMany;
		Purchased = true;

		// Apply multipliers!
		currentTrianglePrice = Mathf.RoundToInt (currentTrianglePrice * trianglePriceMultiplier);
		currentSquarePrice = Mathf.RoundToInt (currentSquarePrice * squarePriceMultiplier);
		currentHexagonPrice = Mathf.RoundToInt (currentHexagonPrice * hexagonPriceMultiplier);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		StoreAnimator.SetBool ("Hover", true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		StoreAnimator.SetBool ("Hover", false);
	}

	void Start()
	{
		currentTrianglePrice = baseTrianglePrice;
		currentSquarePrice = baseSquarePrice;
		currentHexagonPrice = baseHexagonPrice;
	}

	void Update()
	{
		UICoinCount.text = Coins.ToString ();
		TriangleBuyButtonLabel.text = currentTrianglePrice.ToString ();
		SquareBuyButtonLabel.text = currentSquarePrice.ToString ();
		HexagonBuyButtonLabel.text = currentHexagonPrice.ToString ();
	}
}
