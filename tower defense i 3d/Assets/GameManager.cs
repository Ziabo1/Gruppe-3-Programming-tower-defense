using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject GameOverUI; // UI object for displaying the game over screen
    public GameObject completeLevelUI; // UI object for displaying the level completion screen

    void Start()
    {
        GameIsOver = false; // Set the initial state of the game to not over
    }

    public void Update()
    {
        if (GameIsOver)
            return;

        if (PlayerStats.Lives <= 0)
        {
            Debug.Log("Game over!");
            EndGame(); // Call the EndGame function when the player's lives reach zero
        }
    }

    public void EndGame()
    {
        GameIsOver = true; // Set the game over state to true
        GameOverUI.SetActive(true); // Show the game over UI
    }

    public void WinLevel()
    {
        GameIsOver = true; // Set the game over state to true
        completeLevelUI.SetActive(true); // Show the level completion UI
    }
}