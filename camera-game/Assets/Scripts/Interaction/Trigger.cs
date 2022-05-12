using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent whileTrigger;
    protected bool _activated = false;
    // Update is called once per frame
    public virtual void Update()
    {
        if (_activated){
            WhileTrigger();
        }
    }
    public virtual void WhileTrigger(){
        whileTrigger.Invoke();
    }
    public virtual void OnTriggerStart()
    {
        _activated = true;
        onTriggerEnter.Invoke();
    }

    public virtual void OnTriggerExit(){
        _activated = false;
        onTriggerExit.Invoke();
    }
}
