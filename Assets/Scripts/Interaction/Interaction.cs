using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class allows configuration of label, input method and custom interaction sequence to be configured in the inspector
/// </summary>
[System.Serializable]
public class Interaction
{
    /// <summary>
    /// how the users identifies the interaction
    /// </summary>
    public string label;

    /// <summary>
    /// how the user triggers the action (identifier from the Input Manager)
    /// </summary>
    public string method;

    /// <summary>
    /// what happens when this interaciton happens
    /// </summary>
    public UnityEvent onInteract;

    /// <summary>
    /// Determines whether the user has inputted the required input to trigger the action
    /// </summary>
    /// <returns>True or false</returns>
    public bool Triggered(){
        return Input.GetAxis(method) != 0;
    }

    /// <summary>
    /// Runs the interaction sequence
    /// </summary>
    public void Interact(){
        onInteract.Invoke();
    }
}
