using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Objektet, der skal spawnes
    public int numberOfObjectsToSpawn; // Antallet af objekter, der skal spawnes
    public int objectsPerSpawn = 5; // Antallet af objekter pr. spawning
    public Transform spawnLocation; // Spawndestinationen
    public float spawnDelay = 5f; // Forsinkelse mellem hver spawning (ændret til 5 sekunder)
    public Vector3 despawnPosition; // Positionen, hvor objekterne skal forsvinde
    bool hasrun = false;


    private int spawnedObjectsCount = 0; // Det aktuelle antal spawne objekter

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

                if (spawnedObject != null)
                {
                    Rigidbody spawnedRigidbody = spawnedObject.AddComponent<Rigidbody>();

                    // Configure Rigidbody properties as needed
                    spawnedRigidbody.mass = 1f;
                    spawnedRigidbody.drag = 0.5f;
                    spawnedRigidbody.angularDrag = 0.5f;
                    spawnedRigidbody.useGravity = true;
                    spawnedRigidbody.isKinematic = false;
                    spawnedRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    // Tilføj script til det spawned objekt, der fjerner det, når det når den ønskede position
                    DespawnWhenReached script = spawnedObject.AddComponent<DespawnWhenReached>();
                    script.despawnPosition = despawnPosition;
                }
            }

            spawnedObjectsCount += objectsToSpawn;



            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
public class DespawnWhenReached : MonoBehaviour
{
        public Vector3 despawnPosition; // Positionen, hvor objektet skal forsvinde


        private void Update()
        {
            if (transform.position == despawnPosition)
            {
                Destroy(gameObject);
            }
        }
}