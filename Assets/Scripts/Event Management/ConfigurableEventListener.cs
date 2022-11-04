using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class ConfigurableEventListener : MonoBehaviour
{
    [System.Serializable]
    public class Listener
    {
        public string eventName;
        public UnityEvent onCallback;
        public void Callback()
        {
            onCallback.Invoke();
        }
        public void Initialise()
        {
            EventDispatcher.Instance.AddEventListener(eventName,(Action)Callback);
        }
    }

    public List<Listener> listeners = new();
    private void Start()
    {
        foreach (Listener listener in listeners)
        {
            listener.Initialise();
        }
    }
}
