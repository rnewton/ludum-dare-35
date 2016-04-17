using UnityEngine;using System.Collections;using System;public class GameManager : MonoBehaviour {    public float spawnTimeout = 10f;    public float spawnModPerWave = 0.95f;    public float timeBetweenWaves = 2f;    public int blobsPerWave = 10;    public float blobModPerWave = 1.1f;    public GameObject blobPrefab;    public GameObject environment;    private GameObject[] spawns;    private float spawnTimer;    private int blobCount = 0;    private int blobsThisWave;    private float spawnTimeoutThisWave;    private int waveCount = 1;    private bool transitioning = false;    // Use this for initialization    void Start () {        spawns = GameObject.FindGameObjectsWithTag("BlobSpawn");        spawnTimer = spawnTimeout;        blobsThisWave = blobsPerWave;        spawnTimeoutThisWave = spawnTimeout;	}		// Update is called once per frame	void Update () {        if (transitioning)
        {
            waitBetweenWaves();
        }
        else if (blobCount >= blobsThisWave)
        {
            spawnTimer = 0f;            transitioning = true;        } else
        {
            spawnBlobs();
        }    }    private void waitBetweenWaves()
    {
        spawnTimer += Time.deltaTime;

        Debug.Log("waiting...");
        if (spawnTimer >= timeBetweenWaves)
        {
            startNewRound();
        }
    }    private void startNewRound()
    {
        Debug.Log("new round go!");
        waveCount++;
        spawnTimer = 0;
        blobsThisWave = (int)Math.Ceiling((double)blobsPerWave * (double)waveCount * (double)blobModPerWave);
        spawnTimeoutThisWave = spawnTimeoutThisWave * spawnModPerWave;
        blobCount = 0;
        transitioning = false;
    }    private void spawnBlobs()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTimeout) {
            GameObject spawn = spawns[UnityEngine.Random.Range(0, spawns.Length)];

            GameObject enemy = (GameObject)Instantiate(blobPrefab, 
                new Vector3(spawn.transform.position.x, spawn.transform.position.y, -5), 
                Quaternion.identity
            );
            enemy.tag = "Enemy";
            enemy.GetComponent<Rigidbody2D>().AddRelativeForce(-enemy.transform.position);

            spawnTimer = 0f;
            blobCount++;
        }
    }}