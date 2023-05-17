using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
<<<<<<< HEAD
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
=======
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
>>>>>>> effe46f7f4cba68ac5b44e181e053c9bdc6b8858
    }

    // Progress the tracker towards the next waypoint
    public void ProgressTracker()
    {
<<<<<<< HEAD
        // Check if the tracker should progress
        if (Vector3.Distance(tracker.transform.position, transform.position) > lookAhead)
            return;

        // Check if the current waypoint is reached
=======
        // Check if the tracker is far enough from the object's position to proceed
        if (Vector3.Distance(tracker.transform.position, transform.position) > lookAhead)
            return;

        // Check if the tracker has reached the current waypoint
>>>>>>> effe46f7f4cba68ac5b44e181e053c9bdc6b8858
        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 1)
            currentWP = (currentWP + 1) % waypoints.Length; // Move to the next waypoint, loop back to the start if at the end

<<<<<<< HEAD
        // Rotate the tracker towards the next waypoint and move it forward
        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 1) * Time.deltaTime);
=======
        tracker.transform.LookAt(waypoints[currentWP].transform); // Rotate the tracker to face the next waypoint
        tracker.transform.Translate(0, 0, (speed + 1) * Time.deltaTime); // Move the tracker towards the next waypoint
>>>>>>> effe46f7f4cba68ac5b44e181e053c9bdc6b8858
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

<<<<<<< HEAD
        // Check if the enemy's health is depleted
=======
        // Check if the object has reached the last waypoint
        if (currentWP == waypoints.Length - 1)
        {
            EndPath(); // Call the EndPath function
        }

        // Check if the object's health has reached zero
>>>>>>> effe46f7f4cba68ac5b44e181e053c9bdc6b8858
        if (Health <= 0)
        {
            Destroy(gameObject); // Destroy the game object
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
<<<<<<< HEAD
        // Decrease the player's lives
        PlayerStats.Lives--;

        // Check if the player's lives are depleted
=======
        PlayerStats.Lives--; // Decrement the player's lives

        // Check if the player has no remaining lives
>>>>>>> effe46f7f4cba68ac5b44e181e053c9bdc6b8858
        if (PlayerStats.Lives <= 0)
        {
            Debug.Log("Game over!");

<<<<<<< HEAD
            // Retrieve the game manager and end the game
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();
=======
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame(); // Call the EndGame function from the GameManager
>>>>>>> effe46f7f4cba68ac5b44e181e053c9bdc6b8858

            // Destroy the game object associated with this script
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Life lost. Remaining lives: " + PlayerStats.Lives);

<<<<<<< HEAD
            // Decrease the count of alive enemies
            ObjectSpawner.EnemiesAlive--;

            // Destroy the game object associated with this script
            Destroy(gameObject);
=======
            ObjectSpawner.EnemiesAlive--; // Decrement the number of alive enemies

            Destroy(gameObject); // Destroy the game object
>>>>>>> effe46f7f4cba68ac5b44e181e053c9bdc6b8858
        }
    }
}