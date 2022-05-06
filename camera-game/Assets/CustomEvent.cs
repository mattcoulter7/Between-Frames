using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvent : MonoBehaviour
{
    public enum Event {
        OnAwake=0,
        OnStart=1
    }

    [System.Serializable]
    public class EventBind {
        public Event eventType;
        public UnityEvent unityEvent; 
    }

    public List<EventBind> bindings = new List<EventBind>();

    UnityEvent GetUnityEvent(Event eventType) {
        foreach(var binding in bindings) {
            if (binding.eventType == eventType)
            {
                return binding.unityEvent;
            }
        }
        return null;
        
    }

    void HandleInvoke(Event eventType){
        UnityEvent e = GetUnityEvent(eventType);
        if (e != null){ 
            e.Invoke();
        }
    }

    private void Awake()
    {
        HandleInvoke(Event.OnAwake);
    }

    // Start is called before the first frame update
    void Start()
    {
        HandleInvoke(Event.OnStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
