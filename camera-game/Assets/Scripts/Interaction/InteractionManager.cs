using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public List<Interaction> currentInteractions;

    void Start(){
        EventDispatcher.AddEventListener("InteractionEntered", (Action<Interactable>)AddInteractions);
        EventDispatcher.AddEventListener("InteractionExited", (Action<Interactable>)RemoveInteractions);
    }
    public void AddInteractions(Interactable interactable){
        foreach (Interaction interaction in interactable.interactions){
            currentInteractions.Add(interaction);
        }

        EventDispatcher.Dispatch("InteractionsChanged",this);
    }

    public void RemoveInteractions(Interactable interactable){
        foreach (Interaction interaction in interactable.interactions){
            currentInteractions.Remove(interaction);
        }

        EventDispatcher.Dispatch("InteractionsChanged",this);
    }
}
