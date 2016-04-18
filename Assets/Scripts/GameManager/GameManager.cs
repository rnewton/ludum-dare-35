using UnityEngine;using UnityEngine.SceneManagement;using UnityEngine.UI;using System.Collections;using System.Collections.Generic;using System;public class GameManager : MonoBehaviour {    public int maxActiveBlobs = 8;    public float spawnTimeoutMinimum = 0.1f;    public float spawnTimeout = 10f;    public float spawnModPerWave = 0.95f;    public float timeBetweenWaves = 2f;    public float notificationTimeout = 3f;    public int blobsPerWave = 10;    public float blobModPerWave = 1.1f;    public GameObject blobPrefab;    public GameObject notificationText;    public GameObject gameStatsTracker;    public GameObject pausePanel;    private GameObject[] spawns;    private List<GameObject> expiredBlobs;    private int numPieces;    private float spawnTimer;    private int blobCount = 0;    private int blobsThisWave;    private float spawnTimeoutThisWave;    private int waveCount = 1;    private int activeBlobs = 0;    public bool transitioning = false;    private float notificationTimer = 0f;	private PlayerAttacks playerAttacks;    // Use this for initialization    void Start () {        expiredBlobs = new List<GameObject>();        spawns = GameObject.FindGameObjectsWithTag("BlobSpawn");        spawnTimer = spawnTimeout;        blobsThisWave = blobsPerWave;        spawnTimeoutThisWave = spawnTimeout;		playerAttacks = GameObject.Find ("Player").GetComponent<PlayerAttacks> ();        numPieces = GameObject.Find("Dodecagram").GetComponentsInChildren<DodecagramPiece>().Length;	}		// Update is called once per frame	void Update () {        if (transitioning)
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
            if (numPieces == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else if (notificationText.active == true)
            {
                notificationText.active = false;
            }
        }        for (int i = 0; i < expiredBlobs.Count; i++)
        {
            Destroy(expiredBlobs[i]);
        }        expiredBlobs.Clear();    }    public void blobEscapedWithPiece()
    {
        numPieces--;
        decrementActiveBlobs();

        if (numPieces == 0)
        {
            notificationTimer = 2 * notificationTimeout;
            notificationText.GetComponent<Text>().text = "You have failed!";
        } else
        {
            notificationTimer = notificationTimeout;
        }
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
        if (spawnTimeoutThisWave < spawnTimeoutMinimum)
        {
            spawnTimeoutThisWave = spawnTimeoutMinimum;
        }

        blobCount = 0;
        transitioning = false;        gameStatsTracker.GetComponent<GameStats>().roundsSurvived++;		playerAttacks.NewWave ();
    }    private void spawnBlobs()
    {
        spawnTimer += Time.deltaTime;

        if (activeBlobs > maxActiveBlobs)
        {
            // performance optimization; wait til the # of active blobs is reduced
            return;
        }
        else if (spawnTimer >= spawnTimeoutThisWave) {
            GameObject spawn = spawns[UnityEngine.Random.Range(0, spawns.Length)];

            GameObject enemy = (GameObject)Instantiate(blobPrefab, 
                new Vector3(spawn.transform.position.x, spawn.transform.position.y, -5), 
                Quaternion.identity
            );
            enemy.tag = "Enemy";
            enemy.layer = 8;
            enemy.GetComponent<Rigidbody2D>().AddForce(-enemy.transform.position.normalized * enemy.GetComponent<BlobBehavior>().speed);
            enemy.GetComponent<BlobBehavior>().gameManager = this;

            spawnTimer = 0f;
            blobCount++;
            activeBlobs++;
        }
    }    public void decrementActiveBlobs()
    {
        activeBlobs--;
    }    public void queueBlobDeletion(GameObject blob)
    {
        expiredBlobs.Add(blob);
    }    public void togglePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;            pausePanel.active = false;
        } else
        {
            Time.timeScale = 0;            pausePanel.active = true;        }    }}