using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Interaction
{
    public string Label; // how the users identifies the interaction
    public List<KeyCode> methods; // how the user triggers the action
    public UnityEvent onInteract; // what happens when this interaciton happens

    public bool Triggered(){
        foreach (KeyCode method in methods){
            if (Input.GetKey(method)){
                return true;
            }
        }
        return false;
    }

    public void Interact(){
        this.onInteract.Invoke();
    }
}
