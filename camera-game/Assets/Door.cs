using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent onDoorEnter;
    public string targetSceneName;
    public float waitTime;

    public void EnterDoor(){
        onDoorEnter.Invoke();
    }
}
