using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// class used for sending custom data through animation callbacks
[CreateAssetMenu(fileName = "CustomAnimationEventMessage", menuName = "Custom Animation Event Message", order = 51)]
public class CustomAnimationEventMessage : ScriptableObject
{
    public DynamicModifier valueSetter;
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

// OBSERVES FUNCTION CHANGES
[System.Serializable]
public class AnimationEventRecord : AnimationRecord
{
    public DynamicModifier valueSetter; // Used for setting the value property
    public string functionName; // function called to update variable
    public List<AnimationEvent> animationEvents;

    public override object lastValue => animationEvents.Count > 0 ? ((CustomAnimationEventMessage)(animationEvents[animationEvents.Count - 1].objectReferenceParameter)).value : null;
    public override int frameCount => animationEvents.Count;

    // Update is called once per frame
    public override void OnInitialise()
    {
        base.OnInitialise();
        valueSetter.OnInitialise();
    }

    public override void Record(bool optimise = true)
    {
        // list to values from a given path (could be a variable or a method that returns something)
        object value = valueGetter.GetValue();

        if (optimise && Equals(value, lastValue)) return;

        if (lastValue != null)
        {
            // create an event frame right before the new frame with existing value to fix interpolation issues.
            animationEvents.Add(CreateAnimationEvent(lastValue,-0.1f));
        }
        // create the actual event frame
        animationEvents.Add(CreateAnimationEvent(value));
    }

    public override void Apply(AnimationClip clip)
    {
        clip.events = animationEvents.ToArray();
    }

    private AnimationEvent CreateAnimationEvent(object value,float timeOffset = 0f)
    {
        AnimationEvent e;
        e = new AnimationEvent();
        e.time = Time.time + timeOffset;
        e.functionName = "OnEventFire";

        CustomAnimationEventMessage mediator;
        mediator = ScriptableObject.CreateInstance<CustomAnimationEventMessage>();
        mediator.valueSetter = valueSetter;
        mediator.value = value;
        e.objectReferenceParameter = mediator;

        return e;
    }
}
