using UnityEngine;using System.Collections;using System;public class GameManager : MonoBehaviour {    public float spawnTimeout = 10f;    public float spawnModPerWave = 0.95f;    public float timeBetweenWaves = 2f;    public float notificationTimeout = 3f;    public int blobsPerWave = 10;    public float blobModPerWave = 1.1f;    public GameObject blobPrefab;    public GameObject environment;    public GameObject notificationText;    private GameObject[] spawns;    private float spawnTimer;    private int blobCount = 0;    private int blobsThisWave;    private float spawnTimeoutThisWave;    private int waveCount = 1;    public bool transitioning = false;    private float notificationTimer = 0f;	private PlayerAttacks playerAttacks;    // Use this for initialization    void Start () {        spawns = GameObject.FindGameObjectsWithTag("BlobSpawn");        spawnTimer = spawnTimeout;        blobsThisWave = blobsPerWave;        spawnTimeoutThisWave = spawnTimeout;		playerAttacks = GameObject.Find ("Player").GetComponent<PlayerAttacks> ();	}		// Update is called once per frame	void Update () {        if (transitioning)
        {
            waitBetweenWaves();
        }
        else if (blobCount >= blobsThisWave)
        {
            spawnTimer = 0f;            transitioning = true;        } else
        {
            spawnBlobs();
        }        if (notificationTimer > 0)
        {
            notificationTimer -= Time.deltaTime;

            if (notificationText.active == false)
            {
                notificationText.active = true;
            }
        } else
        {
            if (notificationText.active == true)
            {
                notificationText.active = false;
            }
        }    }    public void notify()
    {
        notificationTimer = notificationTimeout;
    }	public int BlobsThisWave()	{		return blobsThisWave;	}	public int WaveCount()	{		return waveCount;	}	public float TimeLeftInWave()	{		return timeBetweenWaves - spawnTimer;	}    private void waitBetweenWaves()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= timeBetweenWaves)
        {
            startNewRound();
        }
    }    private void startNewRound()
    {
        waveCount++;
        spawnTimer = 0;
        blobsThisWave = (int)Math.Ceiling((double)blobsThisWave * (double)blobModPerWave);
        spawnTimeoutThisWave = spawnTimeoutThisWave * spawnModPerWave;
        blobCount = 0;
        transitioning = false;		playerAttacks.NewWave ();
    }    private void spawnBlobs()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTimeoutThisWave) {
            GameObject spawn = spawns[UnityEngine.Random.Range(0, spawns.Length)];

            GameObject enemy = (GameObject)Instantiate(blobPrefab, 
                new Vector3(spawn.transform.position.x, spawn.transform.position.y, -5), 
                Quaternion.identity
            );
            enemy.tag = "Enemy";
            enemy.layer = 8;
            enemy.GetComponent<Rigidbody2D>().AddForce(-enemy.transform.position.normalized * enemy.GetComponent<BlobBehavior>().speed);

            spawnTimer = 0f;
            blobCount++;
        }
    }}