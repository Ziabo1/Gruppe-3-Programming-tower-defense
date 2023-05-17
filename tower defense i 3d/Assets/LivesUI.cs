using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText; // Reference to the TextMeshProUGUI component for displaying lives

    void Update()
    {
        livesText.text = PlayerStats.Lives.ToString() + " LIVES "; // Update the lives text with the current number of lives from PlayerStats
    }
}