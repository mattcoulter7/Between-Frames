using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles display all the current interactions from InteractionManager onto the Panel
/// </summary>
[RequireComponent(typeof(ActionUIBindings))]
public class InteractionMenu : MonoBehaviour
{
    public ActionUIBindings actionUIBindings;

    // Start is called before the first frame update
    private void Start()
    {
        EventDispatcher.Instance.AddEventListener("InteractionsChanged", (Action<InteractionManager>)OnInteractionsChanged);
        actionUIBindings = GetComponent<ActionUIBindings>();
    }

    private void ClearAllInteractions()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ShowAllInteractions(InteractionManager interactionManager)
    {
        RectTransform rectTransform = (RectTransform)transform;
        float targetHeight = rectTransform.position.y;
        
        foreach (Interaction interaction in interactionManager.currentInteractions)
        {
            GameObject interactionPrefab = actionUIBindings.GetPrefab(interaction.label);
            if (interactionPrefab == null) continue;

            Vector3 targetPos = new Vector3(rectTransform.position.x,targetHeight,rectTransform.position.z);
            GameObject interactionInstance = Instantiate(interactionPrefab,transform);
            interactionInstance.transform.position = targetPos;

            float height = ((RectTransform)interactionInstance.transform).rect.height;

            targetHeight -= height;
        }
    }

    private void OnInteractionsChanged(InteractionManager interactionManager)
    {
        Debug.Log("Updating Interaction UI!");
        ClearAllInteractions();
        ShowAllInteractions(interactionManager);
    }
}
