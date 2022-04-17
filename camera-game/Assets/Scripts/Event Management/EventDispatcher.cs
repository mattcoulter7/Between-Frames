using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

static public class EventDispatcher
{
    // handler class for managed instance/static functions

    // dataset for storing functions against an identifier
    static Dictionary<string, List<Delegate>> handlers = new Dictionary<string, List<Delegate>> { };
    
    // returns functions under an identifier, has safety check in place
    static List<Delegate> GetHandlerList(string identifier){
        List<Delegate> existing;
        try {
            existing = handlers[identifier];
        } catch(Exception e){
            handlers.Add(identifier,new List<Delegate>());
            existing = handlers[identifier];
        }
        return existing;
    }

    // Add a new event listener to call a function
    static public void AddEventListener(string identifier, Delegate func)
    {
        List<Delegate> existing = GetHandlerList(identifier);
        existing.Add(func);
    }
    
    // dispatch the function
    static public List<object> Dispatch(string identifier, params object[] paramSet)
    {
        List<object> resultSet = new List<object>();

        List<Delegate> callbacks = handlers[identifier];
        foreach (Delegate callback in callbacks)
        {
            callback.DynamicInvoke(paramSet);
        }

        return resultSet;
    }

    static MethodInfo GetMethodInfo(Delegate d)
    {
        return d.Method;
    }
}