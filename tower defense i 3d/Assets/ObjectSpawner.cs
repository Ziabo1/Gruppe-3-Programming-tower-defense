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
    private float countdown = 2f; // Tælleren til nedtællingen mellem bølger.
    public GameManager gameManager; // Reference til GameManager-scriptet.
    //public Text waveCountdownText;
    public List<GameObject> spawnedObjectList; // En liste over de spawnede objekter.




    private int waveIndex = 0; // Indekset for den aktuelle bølge.
    private int spawnedObjectsCountPerWave = 0; // Det aktuelle antal spawne objekter

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return; // Stopper opdateringen, hvis der stadig er fjender i live.
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false; // Deaktiverer scriptet, når alle bølger er afsluttet.
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnObjects()); // Starter coroutine for at spawne objekter.
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
            yield return null; // Returnerer null, hvis Start-metoden allerede er kørt.
        else
        {
            yield return StartCoroutine(SpawnObjects()); // Starter coroutine for at spawne objekter.
        }
        hasrun = true;
    }
    private IEnumerator SpawnObjects()
    {

        if (IsListEmpty(spawnedObjectList)) // Kontrollerer, om listen over spawne objekter er tom.
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
                    GameObject spawnedObject = Instantiate(getWaveobjectToSpawn(), spawnLocation.position, Quaternion.identity); // Spawner et objekt og gemmer referencen.
                    spawnedObject.GetComponent<FollowWP>().Health = getWaveobjectHealth(); // Tilpasser objektets sundhed ved at hente værdien fra den aktuelle bølge.
                    spawnedObjectList.Add(spawnedObject); // Tilføjer det spawne objekt til listen.

                }

                spawnedObjectsCountPerWave += objectsToSpawn;

                yield return new WaitForSeconds(spawnDelay); // Venter på spawnDelay mellem hver iteration af objektspawningen.
            }
            waveIndex++;
        }

    }

    private int numberOfWaveObjectsToSpawn() {
        return waves[waveIndex].Count; // Returnerer antallet af objekter, der skal spawnes for den aktuelle bølge.
    }

    private int getWaveobjectHealth()
    {
        return waves[waveIndex].Health; // Returnerer sundhedsværdien for objekterne i den aktuelle bølge.
    }

    private GameObject getWaveobjectToSpawn() { 
      return waves[waveIndex].enemy; // Returnerer objektet, der skal spawnes for den aktuelle bølge.
    }

    private bool IsListEmpty(List<GameObject> spawnedObjectListPerWave)
    {
        if (spawnedObjectListPerWave.Any(i => i != null))
        {
            return false; // Returnerer false, hvis der er mindst et objekt i listen.
        }
        spawnedObjectListPerWave.Clear();
        spawnedObjectsCountPerWave = 0; // Nulstiller tælleren for spawne objekter og rydder listen.
        return true; // Returnerer true, hvis listen er tom.
    }
}