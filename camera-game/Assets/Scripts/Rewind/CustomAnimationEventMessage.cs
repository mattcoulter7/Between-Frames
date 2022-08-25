using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Class used for sending custom data through animation callbacks
/// 
/// Due to Unity's limitation of not being able to send through any object parameter, and only having 1 parameter, 
/// we send through a custom class into the objectReferenceParameter of the AnimationEvent
/// 
/// Since we have a valueSetter inside of this CustomAnimationEventMessage which can be set into the objectReferenceParameter as it inherits ScriptableObject,
/// We break the limitations mentioned above
/// </summary>
[CreateAssetMenu(fileName = "CustomAnimationEventMessage", menuName = "Custom Animation Event Message", order = 51)]
public class CustomAnimationEventMessage : ScriptableObject
{
    /// <summary>
    /// The custom configured value setter used instead of Unity's native one
    /// </summary>
    public DynamicModifier valueSetter;

    /// <summary>
    /// An object getter/setter for value which lines up with Unity's AnimationEvent parameters
    /// </summary>
    public object value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            try
            {
                _boolValue = (bool)value;
            }
            catch (InvalidCastException e) { }
            try
            {
                _stringValue = (string)value;
            }
            catch (InvalidCastException e) { }
            try
            {
                _intValue = (int)value;
            }
            catch (InvalidCastException e) { }
            try
            {
                _floatValue = (float)value;
            }
            catch (InvalidCastException e) { }
        }
    }

    // debug properties
    private object _value;
    private bool _boolValue;
    private string _stringValue;
    private int _intValue;
    private float _floatValue;
}
