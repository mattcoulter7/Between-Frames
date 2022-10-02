using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerCursor : MonoBehaviour
{
    public EventSystem eventSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (eventSystem == null)
        {
            eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        }
      
        if (eventSystem.currentSelectedGameObject == null && eventSystem.firstSelectedGameObject != null)
        {
            Debug.Log("Controller cursor debug move");
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }
    }


}
