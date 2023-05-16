using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gameover : MonoBehaviour
{
	// Start is called before the first frame update
	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;

	public void Retry()
	{
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		sceneFader.FadeTo(menuSceneName);
	}

}
