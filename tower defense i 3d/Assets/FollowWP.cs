using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints; // Array of waypoints for the object to follow
    private int currentWP = 0; // Index of the current waypoint
    private GameObject tracker; // Object used to track the waypoints
    public float speed = 100.0f; // Speed at which the object moves
    public float rotSpeed = 25.0f; // Rotation speed of the object
    public float lookAhead = 200.0f; // Distance to look ahead for the next waypoint
    public int Health = 10; // Health of the object

    public void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder); // Create a primitive object as the tracker
        DestroyImmediate(tracker.GetComponent<Collider>()); // Remove the collider component from the tracker
        tracker.GetComponent<MeshRenderer>().enabled = false; // Disable the mesh renderer of the tracker
        tracker.transform.position = transform.position; // Set the tracker's initial position to the object's position
        tracker.transform.rotation = transform.rotation; // Set the tracker's initial rotation to the object's rotation
    }

    public void ProgressTracker()
    {
        // Check if the tracker is far enough from the object's position to proceed
        if (Vector3.Distance(tracker.transform.position, transform.position) > lookAhead)
            return;

        // Check if the tracker has reached the current waypoint
        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 1)
            currentWP = (currentWP + 1) % waypoints.Length; // Move to the next waypoint, loop back to the start if at the end

        tracker.transform.LookAt(waypoints[currentWP].transform); // Rotate the tracker to face the next waypoint
        tracker.transform.Translate(0, 0, (speed + 1) * Time.deltaTime); // Move the tracker towards the next waypoint
    }

    public void Update()
    {
        ProgressTracker();

        Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotSpeed * Time.deltaTime);
        transform.Translate(0, 0, speed * Time.deltaTime);

        // Check if the object has reached the last waypoint
        if (currentWP == waypoints.Length - 1)
        {
            EndPath(); // Call the EndPath function
        }

        // Check if the object's health has reached zero
        if (Health <= 0)
        {
            Destroy(gameObject); // Destroy the game object
        }
    }

    public void EndPath()
    {
        PlayerStats.Lives--; // Decrement the player's lives

        // Check if the player has no remaining lives
        if (PlayerStats.Lives <= 0)
        {
            Debug.Log("Game over!");

            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame(); // Call the EndGame function from the GameManager

            // Destroy the game object associated with this script
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Life lost. Remaining lives: " + PlayerStats.Lives);

            ObjectSpawner.EnemiesAlive--; // Decrement the number of alive enemies

            Destroy(gameObject); // Destroy the game object
        }
    }
}
