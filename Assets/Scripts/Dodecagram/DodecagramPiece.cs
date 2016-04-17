using UnityEngine;using CoinFlipGames.FSM;using System.Collections;public class DodecagramPiece : MonoBehaviour {    public states state;    public enum states    {        Home,        Stolen,        Dropped    }    private Vector3 homePosition;    private Quaternion homeRotation;	// Use this for initialization	void Start () {        homePosition = gameObject.transform.position;        homeRotation = gameObject.transform.rotation;	}		// Update is called once per frame	void Update () {		}    void OnTriggerEnter2D(Collider2D collider)    {        // allow for 'emergent' behavior; blobs can steal from blobs
        if (state == states.Home || state == states.Stolen)
        {
            collideWithEnemy(collider);
        } else if (state == states.Dropped)
        {
            collideDropped(collider);
        }
    }    void collideWithEnemy(Collider2D collider)
    {
        GameObject obj = collider.gameObject;

        if (obj.CompareTag("Enemy"))
        {
            if (obj.transform.GetComponentInChildren<DodecagramPiece>() == null)
            {
                this.transform.SetParent(obj.transform);
                obj.GetComponent<BlobState>().behaviors.Switch(BlobState.states.Fleeing);
                state = states.Stolen;
            }
        }
    }    void collideDropped(Collider2D collider)
    {
        MovePlayer player = collider.gameObject.GetComponentInParent<MovePlayer>();

        if (player != null)
        {
            gameObject.transform.position = homePosition;
            gameObject.transform.rotation = homeRotation;
            state = states.Home;
        } else
        {
            collideWithEnemy(collider);
        }
    }}