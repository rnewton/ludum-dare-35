using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour {
    public int totalCoinsEarned = 0;
    public int currentCoins = 0;
    public int blobsKilled = 0;
    public int roundsSurvived = 1;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
