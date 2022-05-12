using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public List<Interaction> currentInteractions;

    void Start(){
        EventDispatcher.Instance.AddEventListener("InteractionEntered", (Action<Interactable>)AddInteractions);
        EventDispatcher.Instance.AddEventListener("InteractionExited", (Action<Interactable>)RemoveInteractions);
    }
    public void AddInteractions(Interactable interactable){
        foreach (Interaction interaction in interactable.interactions){
            currentInteractions.Add(interaction);
        }

        EventDispatcher.Instance.Dispatch("InteractionsChanged",this);
    }

    public void RemoveInteractions(Interactable interactable){
        foreach (Interaction interaction in interactable.interactions){
            currentInteractions.Remove(interaction);
        }

        EventDispatcher.Instance.Dispatch("InteractionsChanged",this);
    }
}
