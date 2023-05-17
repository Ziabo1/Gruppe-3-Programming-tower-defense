using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Gameover : MonoBehaviour
{
	// Start is called before the first frame update
	public string menuSceneName = "MainMenu";

	public Text roundsText;

    private void OnEnable()
    {
		roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu()
	{
		Debug.Log("Go To menu.");
	}

}
