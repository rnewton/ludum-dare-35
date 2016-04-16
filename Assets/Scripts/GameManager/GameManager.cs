using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public float spawnTimeout = 10f;
    public GameObject blobPrefab;

    private GameObject[] spawns;
    private float spawnTimer = 0f;

    // Use this for initialization
    void Start () {
        spawns = GameObject.FindGameObjectsWithTag("BlobSpawn");
        Debug.Log("found " + spawns.Length + " spawns");
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTimeout) {
            GameObject spawn = spawns[Random.Range(0, spawns.Length)];

            Debug.Log("here comes ya boy");
            Instantiate(blobPrefab, spawn.transform.position, Quaternion.identity);

            spawnTimer = 0f;
        }
    }
}
