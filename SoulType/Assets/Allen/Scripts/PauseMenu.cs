using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject helpMenu;



    void OnApplicationFocus(bool focusStatus)
    {

        if (focusStatus == false)
        {
            pauseMenu.SetActive(false);
            helpMenu.SetActive(false);
            Time.timeScale = 1f;

        }
    }





    public void OnEnable()
    {

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }

    public void Resume()
    {

        pauseMenu.SetActive(false);
        helpMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;



    }


    public void Help()
    {

        helpMenu.SetActive(true);





    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");





    }









}
