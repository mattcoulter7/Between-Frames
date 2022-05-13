using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject options;
    public GameObject credits;
    public void OnPlay(){
        SceneManager.LoadScene("JacksonTests2");
    }

    public void OnOptions(){
        options.SetActive(true);
        credits.SetActive(false);
        mainMenuUI.SetActive(false);
    }

    public void OnCredits()
    {
        options.SetActive(false);
        credits.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void OnExit(){
        Application.Quit();
    }

    public void Back()
    {
        options.SetActive(false);
        credits.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
