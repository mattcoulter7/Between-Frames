using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// handler class for managed instance/static functions
public class EventDispatcher : MonoBehaviour
{
    public static EventDispatcher Instance;
    void Awake(){
        Instance = this;
    }
    // dataset for storing functions against an identifier
    Dictionary<string, List<Delegate>> handlers = new Dictionary<string, List<Delegate>> { };
    
    // returns functions under an identifier, has safety check in place
    List<Delegate> GetHandlerList(string identifier){
        List<Delegate> existing;
        handlers.TryGetValue(identifier,out existing);
        if (existing == null){
            handlers.Add(identifier,new List<Delegate>());
            existing = handlers[identifier];
        }
        return existing;
    }

    // Add a new event listener to call a function
    public void AddEventListener(string identifier, Delegate func)
    {
        List<Delegate> existing = GetHandlerList(identifier);
        existing.Add(func);
    }
    
    // dispatch the function
    public List<object> Dispatch(string identifier, params object[] paramSet)
    {
        List<object> resultSet = new List<object>();

        List<Delegate> callbacks = GetHandlerList(identifier);
        foreach (Delegate callback in callbacks)
        {
            callback.DynamicInvoke(paramSet);
        }

        return resultSet;
    }
}