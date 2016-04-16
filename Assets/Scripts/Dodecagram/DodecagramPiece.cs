using UnityEngine;using System.Collections;public class DodecagramPiece : MonoBehaviour {	// Use this for initialization	void Start () {		}		// Update is called once per frame	void Update () {		}    void OnTriggerEnter2D(Collider2D collider)    {        GameObject obj = collider.gameObject;        if (obj.CompareTag("Enemy"))        {            if (obj.transform.GetComponentInChildren<DodecagramPiece>() == null)
            {
                this.transform.SetParent(obj.transform);
                obj.GetComponent<BlobState>().behaviors.Switch(BlobState.states.Fleeing);
            }        }    }}