using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// OBSERVES FUNCTION CHANGES
[System.Serializable]
public class AnimationEventRecord
{
    public string functionName; // function called to update variable
    public string baseType; // component type in which value is accessed from
    public string unityProperty; // property path which returns the current value
    public string[] unityMethodParams; // method path which returns the current value
    public List<AnimationEvent> animationEvents;

    private object lastValue;
    private string[] _unityPropertyArray;
    private object _parent;
    private System.Type _type;

    // Update is called once per frame
    public void OnInitialise(GameObject gameObject)
    {
        _unityPropertyArray = unityProperty.Split(".");

        _parent = gameObject.GetComponent(baseType); // hold references to base object which holds the variable
        _type = _parent.GetType(); // store the actual type object
    }
    // Start is called before the first frame update
    public object GetPropertyValueDynamically()
    {
        object parent = _parent;
        System.Type type = _type;

        PropertyInfo property;
        FieldInfo field;
        MethodInfo method;

        foreach (string dir in _unityPropertyArray)
        {
            property = type.GetProperty(dir);
            field = type.GetField(dir);
            method = type.GetMethods(BindingFlags.Public | BindingFlags.Instance).First(m => m.Name == dir && m.GetParameters().Length == unityMethodParams.Length); //type.GetMethod(dir, BindingFlags.Public | BindingFlags.Instance);
            type = (property != null ? property.PropertyType : (field != null ? field.FieldType : (method != null ? method.ReturnType : null)));
            parent = (property != null ? property.GetValue(parent, null) : (field != null ? field.GetValue(parent) : (method != null ? method.Invoke(parent,unityMethodParams) : null)));
        }

        return parent;
    }

    public void Record(bool optimise = true)
    {
        // list to values from a given path (could be a variable or a method that returns something)
        AnimationEvent e = new AnimationEvent();
        e.time = Time.time;
        e.functionName = functionName;

        object value = GetPropertyValueDynamically();

        if (optimise && (value == lastValue)) return;

        bool validValue = false;

        // run once but can break out at anytime
        while (true)
        {
            try
            {
                e.floatParameter = (float)value;
                validValue = true;
                break; // only one parameter is allowed to be set against event
            }
            catch (InvalidCastException) { }
            try
            {
                e.intParameter = (int)value;
                validValue = true;
                break; // only one parameter is allowed to be set against event
            }
            catch (InvalidCastException) { }
            try
            {
                e.stringParameter = (string)value;
                validValue = true;
                break; // only one parameter is allowed to be set against event
            }
            catch (InvalidCastException) { }
            try
            {
                e.objectReferenceParameter = (UnityEngine.Object)value;
                validValue = true;
                break; // only one parameter is allowed to be set against event
            }
            catch (InvalidCastException) { }
            try
            {
                bool boolParameter = (bool)value;

                // making it here means the value is a boolean as it didn't throw an InvalidCastException
                validValue = true;

                // due to Unity limitations of not storing a boolean, booleans can only be used for setting Animator booleans
                e.stringParameter = unityMethodParams[unityMethodParams.Length - 1];

                // these Function exists within RewindInstance and act as a mediator for Animator.SetBool(stringParameter)
                e.functionName = boolParameter ? "BooleanRedirectTrue" : "BooleanRedirectFalse";
                break; // only one parameter is allowed to be set against event
            }
            catch (InvalidCastException) { }
            break;
        }

        if (validValue)
        {
            lastValue = value;
            animationEvents.Add(e);
        }
    }

    public void Apply(AnimationClip clip)
    {
        clip.events = animationEvents.ToArray();
    }
}
