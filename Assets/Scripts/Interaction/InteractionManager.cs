using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a reference to all of the current Interactions that the user can perform
/// Used for the UI as the player can be inside of multiple interaction collider, all of the interaction options need to show
/// </summary>
public class InteractionManager : MonoBehaviour
{
    /// <summary>
    /// List of all the current possible interactions
    /// </summary>
    public List<Interaction> currentInteractions;

    /// <summary>
    /// Adds multiple interaction options to the current active list
    /// </summary>
    /// <param name="interactable">The component which holds the interactions with a specific object</param>
    public void AddInteractions(Interactable interactable){
        foreach (Interaction interaction in interactable.interactions){
            currentInteractions.Add(interaction);
        }

        EventDispatcher.Instance.Dispatch("InteractionsChanged",this);
    }

    /// <summary>
    /// Removes multiple interaction options to the current active list
    /// </summary>
    /// <param name="interactable">The component which holds the interactions with a specific object</param>
    public void RemoveInteractions(Interactable interactable){
        foreach (Interaction interaction in interactable.interactions){
            currentInteractions.Remove(interaction);
        }

        EventDispatcher.Instance.Dispatch("InteractionsChanged",this);
    }
    private void Start()
    {
        EventDispatcher.Instance.AddEventListener("InteractionEntered", (Action<Interactable>)AddInteractions);
        EventDispatcher.Instance.AddEventListener("InteractionExited", (Action<Interactable>)RemoveInteractions);
    }
}
