using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWP = 0;
    private GameObject tracker;
    public float speed = 100.0f;
    public float rotSpeed = 25.0f;
    public float lookAhead = 200.0f;
    public int Health = 10;
    public void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;
    }

    public void ProgressTracker()
    {
        if (Vector3.Distance(tracker.transform.position, transform.position) > lookAhead)
            return;

        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 1)
            currentWP = (currentWP + 1) % waypoints.Length;

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 1) * Time.deltaTime);
    }

    public void Update()
    {
        ProgressTracker();

        Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotSpeed * Time.deltaTime);
        transform.Translate(0, 0, speed * Time.deltaTime);

        // Tjek om objektet har nået det sidste waypoint
        if (currentWP == waypoints.Length - 1)
        {
            EndPath();
        }
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void EndPath()
    {
        PlayerStats.Lives--;
        if (PlayerStats.Lives <= 0)
        {
            Debug.Log("Game over!");
           
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();
            // Destroy the game object associated with this script
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Life lost. Remaining lives: " + PlayerStats.Lives);
            ObjectSpawner.EnemiesAlive--;
            Destroy(gameObject);
        }
    }
}
