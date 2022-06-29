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
    public object value;
    public CustomAnimationEventMessage(DynamicModifier valueSetter, object value) : base()
    {
        this.valueSetter = valueSetter;
        this.value = value;
    }
}

// OBSERVES FUNCTION CHANGES
[System.Serializable]
public class AnimationEventRecord
{
    public DynamicModifier valueGetter; // Used for fetching the property value
    public DynamicModifier valueSetter; // Used for setting the value property
    public string functionName; // function called to update variable
    public List<AnimationEvent> animationEvents;

    private object lastValue;

    // Update is called once per frame
    public void OnInitialise()
    {
        valueGetter.OnInitialise();
        valueSetter.OnInitialise();
    }

    public void Record(bool optimise = true)
    {
        // list to values from a given path (could be a variable or a method that returns something)
        object value = valueGetter.GetValue();

        
        if (optimise && Equals(value, lastValue))
        {
            return;
        }

        AnimationEvent e;
        CustomAnimationEventMessage mediator;
        if (lastValue != null)
        {
            // create an event frame right before the new frame with existing value to fix interpolation issues.
            e = new AnimationEvent();
            e.time = Time.time - 0.0001f;
            e.functionName = "OnEventFire";


            mediator = ScriptableObject.CreateInstance<CustomAnimationEventMessage>();
            mediator.valueSetter = valueSetter;
            mediator.value = lastValue;
            e.objectReferenceParameter = mediator;

            animationEvents.Add(e);
        }

        // create the actual event frame
        e = new AnimationEvent();
        e.time = Time.time;
        e.functionName = "OnEventFire";

        mediator = ScriptableObject.CreateInstance<CustomAnimationEventMessage>();
        mediator.valueSetter = valueSetter;
        mediator.value = value;
        e.objectReferenceParameter = mediator;

        lastValue = value;
        animationEvents.Add(e);
    }

    public void Apply(AnimationClip clip)
    {
        clip.events = animationEvents.ToArray();
    }
}
