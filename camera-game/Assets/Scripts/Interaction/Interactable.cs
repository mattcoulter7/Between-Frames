using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{
    public List<string> whoCanInteract = new List<string>(); // list of people who can interact
    public List<Interaction> interactions = new List<Interaction>(); // list of available interactions that can happen

    bool canInteract = false; // true when player is in boumds

    bool matchesTag(string tagName)
    {
        return whoCanInteract.Contains(tagName);
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            foreach (Interaction interaction in interactions)
            {
                if (interaction.Triggered())
                {
                    interaction.Interact();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (matchesTag(other.tag))
        {
            canInteract = true;
            EventDispatcher.Dispatch("InteractionEntered",this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (matchesTag(other.tag))
        {
            canInteract = false;
            EventDispatcher.Dispatch("InteractionExited",this);
        }
    }
}
