using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ControllerCursor : MonoBehaviour
{
    public EventSystem eventSystem;
    public PlayerInput playerInput;
    public InputDeviceChangeHandler deviceChangeHandler;
    public GameObject myselected;
    public GameObject myfirstoption;
    public string ControllerType;
    public string currentLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        deviceChangeHandler = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<InputDeviceChangeHandler>();
        //playerInput.actions["Navigate"].performed += ctx => OnNavigate(); // add input context for rewind
        currentLayer = playerInput.currentActionMap.name;
    }

    // Update is called once per frame
    void Update()
    {
        currentLayer = playerInput.currentActionMap.name;
        if (this != null)
        {
            if (eventSystem == null)
            {
                eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
            }
            if(playerInput == null)
            {
                playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
            }

            myselected = eventSystem.currentSelectedGameObject;
            myfirstoption = eventSystem.firstSelectedGameObject;
            ControllerType = InputDeviceChangeHandler.currentDeviceType;

            if (eventSystem.currentSelectedGameObject == null && eventSystem.firstSelectedGameObject != null && ControllerType == "Gamepad")
            {
                Debug.Log("Controller cursor debug move");
                eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
            }
        }

    }
    private void OnDestroy()
    {
        playerInput.actions["Navigate"].performed -= ctx => OnNavigate();
    }

    void OnNavigate()
    {
        //if (this != null)
        //{
        Debug.Log("this is running");
        if(eventSystem.currentSelectedGameObject == null) //&& eventSystem.firstSelectedGameObject != null)
        {
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }
        //}
    }
}
