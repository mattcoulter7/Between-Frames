using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Interaction
{
    public string label; // how the users identifies the interaction
    public string method; // how the user triggers the action
    public UnityEvent onInteract; // what happens when this interaciton happens
    public bool Triggered(){
        return Input.GetAxis(method) != 0;
    }

    public void Interact(){
        this.onInteract.Invoke();
    }
}
