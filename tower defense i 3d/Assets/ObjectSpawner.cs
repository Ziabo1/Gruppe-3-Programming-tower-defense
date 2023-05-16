using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Objektet, der skal spawnes
    public int numberOfObjectsToSpawn; // Antallet af objekter, der skal spawnes
    public int objectsPerSpawn = 5; // Antallet af objekter pr. spawning
    public Transform spawnLocation; // Spawndestinationen
    public float spawnDelay = 5f; // Forsinkelse mellem hver spawning (ændret til 5 sekunder)
    bool hasrun = false;
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public GameManager gameManager;
    //public Text waveCountdownText;
    


    private int waveIndex = 0;
    private int spawnedObjectsCount = 0; // Det aktuelle antal spawne objekter

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnObjects());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

       // waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }


    private IEnumerator Start()
    {


        if (hasrun)
            yield return null;
        else
        {
            yield return StartCoroutine(SpawnObjects());
        }
        hasrun = true;
    }
    private IEnumerator SpawnObjects()
    {
        int spawnIterations = Mathf.CeilToInt((float)numberOfObjectsToSpawn / objectsPerSpawn);
        if (spawnedObjectsCount >= numberOfObjectsToSpawn)
            yield return new WaitForSeconds(spawnDelay);
        for (int i = 0; i < spawnIterations; i++)
        {
            int objectsToSpawn = Mathf.Min(objectsPerSpawn, numberOfObjectsToSpawn - spawnedObjectsCount);

            for (int j = 0; j < objectsToSpawn; j++)
            {
                GameObject spawnedObject = Instantiate(objectToSpawn, spawnLocation.position, Quaternion.identity);
            }

            spawnedObjectsCount += objectsToSpawn;



            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
