using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// This is a generic class for easily binding UNityEvents and MonoBehaviour methods
/// For example, you can make a sound play every time a collision happens without even writing a line of code!
/// </summary>
public class CustomEvent : MonoBehaviour
{
    /// <summary>
    /// The enum for MonoBehaviour methods which values are chosen from in the inspector
    /// </summary>
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

    /// <summary>
    /// This class handles linking of the Event enum trigger (MonoBehaviour method) and an action (UnityEvent) that is configured in the inspector
    /// </summary>
    [System.Serializable]
    public class EventBind {
        /// <summary>
        /// The Event method chosen from the enum which lines up with a MonoBehaviour method
        /// </summary>
        public Event eventType;
        /// <summary>
        /// The UnityEvent which is a set of actions that happen when the method is called
        /// </summary>
        public UnityEvent unityEvent; 
    }

    /// <summary>
    /// The list of bindings to be configured in the inspector (because dictionaries don't show in the inspector!)
    /// </summary>
    public List<EventBind> bindings = new List<EventBind>();

    private UnityEvent GetUnityEvent(Event eventType) {
        foreach(var binding in bindings) {
            if (binding.eventType == eventType)
            {
                return binding.unityEvent;
            }
        }
        return null;
        
    }
    private void HandleInvoke(Event eventType){
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
    private void OnTriggerEnter(Collider other){
        HandleInvoke(Event.OnTriggerEnter);
    }
    private void OnTriggerExit(Collider other){
        HandleInvoke(Event.OnTriggerExit);
    }
    private void OnTriggerStay(Collider other){
        HandleInvoke(Event.OnTriggerStay);
    }
    private void OnCollisionEnter(Collision other){
        HandleInvoke(Event.OnCollisionEnter);
    }
    private void OnCollisionExit(Collision other){
        HandleInvoke(Event.OnCollisionExit);
    }
    private void OnCollisionStay(Collision other){
        HandleInvoke(Event.OnCollisionStay);
    }
}
