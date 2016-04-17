using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
    private GameManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("Environment").GetComponent<GameManager>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject obj = collider.gameObject;

        if (obj.CompareTag("Enemy"))
        {
            if (obj.transform.GetComponentInChildren<DodecagramPiece>() != null)
            {
                Destroy(obj);
                manager.blobEscapedWithPiece();
            }
        }
    }
}
