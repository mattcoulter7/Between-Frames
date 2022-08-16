using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class allows the developer to configure user interactions with an object
/// The interactions are designed to show when the user enter the collider, and hide when the user leaves the collider
/// </summary>
public class Interactable : MonoBehaviour
{
    /// <summary>
    /// List of string tags for what can interact with the object
    /// </summary>
    public List<string> whoCanInteract = new List<string>(); // list of people who can interact

    /// <summary>
    /// List of interactions that are possible with the object, i.e. open, break, pet etc.
    /// </summary>
    public List<Interaction> interactions = new List<Interaction>(); // list of available interactions that can happen

    private bool canInteract = false; // true when player is in boumds

    private bool matchesTag(string tagName)
    {
        return whoCanInteract.Contains(tagName);
    }

    // Update is called once per frame
    private void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if (matchesTag(other.tag))
        {
            canInteract = true;
            EventDispatcher.Instance.Dispatch("InteractionEntered",this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (matchesTag(other.tag))
        {
            canInteract = false;
            EventDispatcher.Instance.Dispatch("InteractionExited",this);
        }
    }
}
