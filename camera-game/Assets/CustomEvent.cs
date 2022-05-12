using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class CustomEvent : MonoBehaviour
{
    public enum Event {
        OnAwake=0,
        OnStart=1,
        OnTriggerEnter=2,
        OnTriggerExit=3,
        OnUpdate=4,
        OnCollisionEnter=5,
        OnCollisionExit=6,
        OnCollisionStay=7,
        OnTriggerStay=8
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
        EventDispatcher.Instance.Dispatch("OnSceneLoad",SceneManager.GetActiveScene());
    }

    // Update is called once per frame
    void Update()
    {
        HandleInvoke(Event.OnUpdate);
    }

    void OnTriggerEnter(Collider other){
        HandleInvoke(Event.OnTriggerEnter);
    }
    void OnTriggerExit(Collider other){
        HandleInvoke(Event.OnTriggerExit);
    }
    void OnTriggerStay(Collider other){
        HandleInvoke(Event.OnTriggerStay);
    }

    void OnCollisionEnter(Collision other){
        HandleInvoke(Event.OnCollisionEnter);
    }
    void OnCollisionExit(Collision other){
        HandleInvoke(Event.OnCollisionExit);
    }
    void OnCollisionStay(Collision other){
        HandleInvoke(Event.OnCollisionStay);
    }
}
