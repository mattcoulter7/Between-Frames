using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles display all the current interactions from InteractionManager onto the Panel
public class InteractionMenu : MonoBehaviour
{
    public GameObject interactionLinePrefab;
    public float heightStep = 50;
    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.AddEventListener("InteractionsChanged", (Action<InteractionManager>)OnInteractionsChanged);
    }

    void ClearAllInteractions()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void ShowAllInteractions(InteractionManager interactionManager)
    {
        RectTransform rectTransform = interactionLinePrefab.GetComponent<RectTransform>();
        float targetHeight = rectTransform.position.y;
        
        foreach (Interaction interaction in interactionManager.currentInteractions)
        {
            Vector3 targetPos = new Vector3(rectTransform.position.x,targetHeight,rectTransform.position.z);
            GameObject interactionLineObject = Instantiate(interactionLinePrefab);
            interactionLineObject.transform.SetParent(transform);
            interactionLineObject.transform.localPosition = targetPos;

            InteractionMenuLine helper = interactionLineObject.GetComponent<InteractionMenuLine>();
            helper.OnInitialise(interaction);

            targetHeight -= heightStep;
        }
    }

    void OnInteractionsChanged(InteractionManager interactionManager)
    {
        Debug.Log("Updating Interaction UI!");
        ClearAllInteractions();
        ShowAllInteractions(interactionManager);
    }
}
