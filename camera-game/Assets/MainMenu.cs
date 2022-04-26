using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    public GameObject credits;
    public void OnPlay(){
        SceneManager.LoadScene("SampleScene");
    }

    public void OnOptions(){
        options.SetActive(true);
        credits.SetActive(false);
        gameObject.SetActive(false);
    }

     public void OnCredits(){
        options.SetActive(false);
        credits.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnExit(){
        Application.Quit();
    }
}
