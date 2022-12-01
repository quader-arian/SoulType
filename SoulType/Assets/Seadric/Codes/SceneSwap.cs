using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    public GameObject wholeCanvas;
    public void StartGame()
    {
        SceneManager.LoadScene("Title");
    }

    public void HelpScreen()
    {
        SceneManager.LoadScene("Help");
    }

    public void Level()
    {
        SceneManager.LoadScene("AITesting");
    }

    public void Back()
    {
        wholeCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("closed"); // remove comment below before building game
        //Application.Quit();
    }
}