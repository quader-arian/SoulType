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
        SceneManager.LoadScene("SoulTypeFacility");
    }

    public void Back()
    {
        wholeCanvas.SetActive(false);
    }
}