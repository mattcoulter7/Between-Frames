using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{
    public List<GameObject> whoCanInteract = new List<GameObject>(); // list of people who can interact
    public List<Interaction> interactions = new List<Interaction>(); // list of available interactions that can happen

    bool canInteract = false; // true when player is in boumds
    // Start is called before the first frame update
    void Start()
    {
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
        if (whoCanInteract.Contains(other.gameObject))
        {
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (whoCanInteract.Contains(other.gameObject))
        {
            canInteract = false;
        }
    }
}
