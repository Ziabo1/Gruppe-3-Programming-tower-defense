using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public string menuSceneName = "MainMenu"; // Name of the menu scene to load

    public Text roundsText; // Reference to the Text component for displaying the number of rounds

    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString(); // Update the rounds text with the current number of rounds
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void Menu()
    {
        Debug.Log("Go To menu."); // Placeholder code for going back to the menu
    }
}