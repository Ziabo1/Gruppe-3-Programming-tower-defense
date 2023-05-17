using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money; // Static variable to store the player's money
    public int startMoney = 400; // Initial amount of money for the player

    public static int Lives; // Static variable to store the player's lives
    public int startLives = 20; // Initial number of lives for the player

    public static int Rounds; // Static variable to store the current round number

    void Start()
    {
        Money = startMoney; // Set the initial amount of money for the player
        Lives = startLives; // Set the initial number of lives for the player
        Rounds = 0; // Set the initial round number to 0
    }

    void Update()
    {
        // Update logic can be added here if needed
    }
}
