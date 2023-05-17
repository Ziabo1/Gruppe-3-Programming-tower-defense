using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject GameOverUI;
    public GameObject completeLevelUI;
    // Start is called before the first frame update
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (GameIsOver)
            return;

        if (PlayerStats.Lives <= 0)
        {
            Debug.Log("Game over!");
            EndGame();
        }
    }
    public void EndGame()
    {
        GameIsOver = true;
        GameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
      completeLevelUI.SetActive(true);
    }

}
