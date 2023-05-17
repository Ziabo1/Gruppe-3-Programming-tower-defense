using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    // Array of waypoints for the enemy to follow
    public GameObject[] waypoints;

    // Index of the current waypoint
    private int currentWP = 0;

    // Tracker object for path progress
    private GameObject tracker;

    // Movement speed of the enemy
    public float speed = 100.0f;

    // Rotation speed of the enemy
    public float rotSpeed = 25.0f;

    // Distance to look ahead for the next waypoint
    public float lookAhead = 200.0f;

    // Health of the enemy
    public float Health = 10;

    public void Start()
    {
        // Create a tracker object and set its initial position and rotation
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;
    }

    // Progress the tracker towards the next waypoint
    public void ProgressTracker()
    {
        // Check if the tracker should progress
        if (Vector3.Distance(tracker.transform.position, transform.position) > lookAhead)
            return;

        // Check if the current waypoint is reached
        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 1)
            currentWP = (currentWP + 1) % waypoints.Length;

        // Rotate the tracker towards the next waypoint and move it forward
        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 1) * Time.deltaTime);
    }

    public void Update()
    {
        // Progress the tracker towards the next waypoint
        ProgressTracker();

        // Rotate the enemy towards the tracker
        Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotSpeed * Time.deltaTime);

        // Move the enemy forward
        transform.Translate(0, 0, speed * Time.deltaTime);

        // Check if the enemy's health is depleted
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Handle enemy damage
    public void TakeDamage(float amount)
    {
        // Decrease the enemy's health by the given amount
        Health -= amount;

        // Check if the enemy's health is depleted
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Called when the enemy reaches the end of the path
    public void EndPath()
    {
        // Decrease the player's lives
        PlayerStats.Lives--;

        // Check if the player's lives are depleted
        if (PlayerStats.Lives <= 0)
        {
            Debug.Log("Game over!");

            // Retrieve the game manager and end the game
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();

            // Destroy the game object associated with this script
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Life lost. Remaining lives: " + PlayerStats.Lives);

            // Decrease the count of alive enemies
            ObjectSpawner.EnemiesAlive--;

            // Destroy the game object associated with this script
            Destroy(gameObject);
        }
    }
}