using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    //public AudioManager theAM;
    //public DestroyAM destroyer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }



    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        //gameObject.SetActive(true);
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Options()
    {
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("CLICKED");
    }


    public void Back()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        //theAM.CarryOverScene = false;
        //destroyer.DestroyIt();
        
        SceneManager.LoadScene("MenuTest");
    }


    public void Exit()
    {
        //AudioManager.CarryOverScene = false;
        Application.Quit();
    }
}
