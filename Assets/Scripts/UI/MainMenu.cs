using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject mainMenuUI;
    public GameObject options;
    public GameObject credits;
    public GameObject myobj;
    public GameObject myobj2;

    public GameObject playbtn;
    public GameObject volumebtn;
    public GameObject credbackbtn;

    public void OnPlay(){
        Debug.Log("play test");
        SceneManager.LoadScene("JacksonTests2");
        
    }

    public void OnOptions(){
        //eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        Debug.Log("op test");
        options.SetActive(true);
        eventSystem.SetSelectedGameObject(null); 
        eventSystem.SetSelectedGameObject(volumebtn);
        eventSystem.firstSelectedGameObject = volumebtn;
        //credits.SetActive(false);
        //mainMenuUI.SetActive(false);
        
    }

    public void OnCredits()
    {
        //eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        Debug.Log("cred test");
        credits.SetActive(true);
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(credbackbtn);
        eventSystem.firstSelectedGameObject = credbackbtn;
        //options.SetActive(false);
        //mainMenuUI.SetActive(false);
        
    }

    public void OnExit(){
        Application.Quit();
    }

    public void Back()
    {
        //eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        Debug.Log("Back test");
        eventSystem.firstSelectedGameObject = playbtn;
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(playbtn);
        //optionToSet3.GetComponent<Button>().OnSelect(null);
        //options.SetActive(false);
        //credits.SetActive(false);
        //mainMenuUI.SetActive(true);
        
    }

    private void Awake()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();

    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            Debug.Log("test test");
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }

        myobj = eventSystem.currentSelectedGameObject;
        myobj2 = eventSystem.firstSelectedGameObject;
        
    }
}
