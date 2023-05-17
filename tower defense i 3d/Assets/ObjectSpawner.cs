using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The GameObject that will be spawned.
    public int numberOfObjectsToSpawn; // The total number of objects to spawn.
    public int objectsPerSpawn = 5; // The number of objects to spawn in each iteration.
    public Transform spawnLocation; // The position where the objects will be spawned.
    public float spawnDelay = 5f; // The delay between each spawning.
    bool hasrun = false; // An array of Wave objects that define the properties of each wave.
    public static int EnemiesAlive = 0; // is a variable that keeps track of the number of enemies currently alive in the game.
    public Wave[] waves; // An array of Wave objects that define the properties of each wave.
    public float timeBetweenWaves = 5f;// The delay between each wave.
    private float countdown = 2f; 
    public GameManager gameManager;
    //public Text waveCountdownText;
    public List<GameObject> spawnedObjectList;



    private int waveIndex = 0;
    private int spawnedObjectsCountPerWave = 0; // Det aktuelle antal spawne objekter

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


    public IEnumerator Start()
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

        if (IsListEmpty(spawnedObjectList))
        {
            // ObjectSpawner.Rounds++;
            
            int spawnIterations = Mathf.CeilToInt((float)numberOfWaveObjectsToSpawn() / objectsPerSpawn);
            if (spawnedObjectsCountPerWave >= numberOfWaveObjectsToSpawn())
                yield return new WaitForSeconds(spawnDelay);
            for (int i = 0; i < spawnIterations; i++)
            {
                int objectsToSpawn = Mathf.Min(objectsPerSpawn, numberOfWaveObjectsToSpawn() - spawnedObjectsCountPerWave);

                for (int j = 0; j < objectsToSpawn; j++)
                {
                    GameObject spawnedObject = Instantiate(getWaveobjectToSpawn(), spawnLocation.position, Quaternion.identity);
                    spawnedObject.GetComponent<FollowWP>().Health = getWaveobjectHealth();
                    spawnedObjectList.Add(spawnedObject);
                    
                }

                spawnedObjectsCountPerWave += objectsToSpawn;

                yield return new WaitForSeconds(spawnDelay);
            }
            waveIndex++;
        }

    }

    private int numberOfWaveObjectsToSpawn() {
        return waves[waveIndex].Count;
    }

    private int getWaveobjectHealth()
    {
        return waves[waveIndex].Health;
    }

    private GameObject getWaveobjectToSpawn() { 
      return waves[waveIndex].enemy;
    }

    private bool IsListEmpty(List<GameObject> spawnedObjectListPerWave)
    {
        if (spawnedObjectListPerWave.Any(i => i != null))
        {
            return false;
        }
        spawnedObjectListPerWave.Clear();
        spawnedObjectsCountPerWave = 0;
        return true;
    }
}
