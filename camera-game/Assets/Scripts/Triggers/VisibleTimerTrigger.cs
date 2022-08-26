using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class VisibleTimerTrigger : TimedTrigger
{
    private bool _currentlyVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        // listen to the flash event
        EventDispatcher.Instance.AddEventListener("Flash", (Action<Image>)OnFlash);
    }
    void OnFlash(Image image)
    {
        if (_currentlyVisible)
        {
            OnTriggerBegin();
        }
    }

    void OnBecameVisible()
    {
        _currentlyVisible = true;
    }

    void OnBecameInvisible()
    {
        _currentlyVisible = false;
    }
}
