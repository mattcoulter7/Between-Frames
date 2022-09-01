using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Public/Subscribe messaging system
/// Is a singleton
/// Call EventDispatcher.Instance.AddEventListener("messageIdentifier",(Action<T1,T2>)function) to subscribe
/// Call EventDispatcher.Instance.Dispatch("messageIdentifier","parametersValue1","parameterValue2") to dispatch
/// </summary>
public class EventDispatcher : MonoBehaviour
{
    /// <summary>
    /// Managed singleton 1 per scene
    /// </summary>
    public static EventDispatcher Instance;

    /// <summary>
    /// Dataset for storing functions against an identifier
    /// </summary>
    private Dictionary<string, List<Delegate>> handlers = new Dictionary<string, List<Delegate>> { };

    /// <summary>
    /// Add a new event listener to call a function
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="func"></param>
    public void AddEventListener(string identifier, Delegate func)
    {
        List<Delegate> existing = GetHandlerList(identifier);
        existing.Add(func);
    }

    /// <summary>
    /// Dispatch the method calling all of the functions which are subscribed to it
    /// </summary>
    /// <param name="identifier">The message identifier</param>
    /// <param name="paramSet">N number of params to pass into the message</param>
    /// <returns>The result of each of the function calls</returns>
    public List<object> Dispatch(string identifier, params object[] paramSet)
    {
        List<object> resultSet = new List<object>();

        List<Delegate> callbacks = GetHandlerList(identifier);
        foreach (Delegate callback in callbacks)
        {
            resultSet.Add(callback.DynamicInvoke(paramSet));
        }

        return resultSet;
    }
    private void Awake(){
        Instance = this;
    }

    /// <summary>
    /// Get all of the functions under an identifier, has safety check in place
    /// </summary>
    /// <param name="identifier">The message identifier</param>
    /// <returns>List of Delegate functions which are subscribed to the message</returns>
    private List<Delegate> GetHandlerList(string identifier){
        List<Delegate> existing;
        handlers.TryGetValue(identifier,out existing);
        if (existing == null){
            handlers.Add(identifier,new List<Delegate>());
            existing = handlers[identifier];
        }
        return existing;
    }
}