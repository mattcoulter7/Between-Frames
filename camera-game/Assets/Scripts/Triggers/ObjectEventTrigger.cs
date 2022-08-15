using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectEventTrigger : EventTrigger
{
    // Utilises EventTrigger configuration but for GameObjects instead
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // handle extra method which do not exist in MonoBehaviour

    }
    BaseEventData GetEventData()
    {
        BaseEventData eventData = new BaseEventData(EventSystem.current);
        //eventData.selectedObject = gameObject;
        return eventData;
    }
    void HandleDelegations(EventTriggerType type)
    {
        foreach (Entry trigger in triggers)
        {
            if (trigger.eventID == type)
            {
                trigger.callback.Invoke(GetEventData());
            }
        }
    }
    void OnMouseOver()
    {
        HandleDelegations(EventTriggerType.Move); // using move as there is no "over" EventTriggerType
    }
    // Called when the mouse is not any longer over the Collider.
    void OnMouseExit()
    {
        HandleDelegations(EventTriggerType.PointerExit);
    }
    // OnMouseDown is called when the user has pressed the mouse button while over the Collider.
    void OnMouseDown()
    {
        HandleDelegations(EventTriggerType.PointerDown);
    }
    // Called every frame while the mouse is over the Collider.
    void OnMouseUp()
    {
        HandleDelegations(EventTriggerType.PointerClick);
    }
    // Called when the mouse enters the Collider.
    void OnMouseEnter()
    {
        HandleDelegations(EventTriggerType.PointerEnter);
    }
    // OnMouseDrag is called when the user has clicked on a Collider and is still holding down the mouse.
    void OnMouseDrag()
    {
        HandleDelegations(EventTriggerType.Drag);
    }
}
