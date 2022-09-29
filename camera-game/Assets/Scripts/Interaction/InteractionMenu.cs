using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles display all the current interactions from InteractionManager onto the Panel
/// </summary>
public class InteractionMenu : MonoBehaviour
{
    /// <summary>
    /// Reference to the UI prefab display a single interaction option
    /// </summary>
    public GameObject interactionLinePrefab;

    /// <summary>
    /// The vertical line spacing between to interaciton line prefabs
    /// </summary>
    public float heightStep = 50;
    // Start is called before the first frame update
    private void Start()
    {
        EventDispatcher.Instance.AddEventListener("InteractionsChanged", (Action<InteractionManager>)OnInteractionsChanged);
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
        RectTransform rectTransform = interactionLinePrefab.GetComponent<RectTransform>();
        float targetHeight = rectTransform.position.y;
        
        foreach (Interaction interaction in interactionManager.currentInteractions)
        {
            Vector3 targetPos = new Vector3(rectTransform.position.x,targetHeight,rectTransform.position.z);
            GameObject interactionLineObject = Instantiate(interactionLinePrefab);
            
            RectTransform t = interactionLineObject.transform as RectTransform;
            t.SetParent(transform);
            t.anchoredPosition = targetPos;

            InteractionMenuLine helper = interactionLineObject.GetComponent<InteractionMenuLine>();
            helper.OnInitialise(interaction);

            targetHeight -= heightStep;
        }
    }

    private void OnInteractionsChanged(InteractionManager interactionManager)
    {
        Debug.Log("Updating Interaction UI!");
        ClearAllInteractions();
        ShowAllInteractions(interactionManager);
    }
}
