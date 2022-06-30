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

    // debug properties
    private bool boolValue;
    private string stringValue;
    private int intValue;
    private float floatValue;
    public CustomAnimationEventMessage(DynamicModifier valueSetter, object value) : base()
    {
        this.valueSetter = valueSetter;
        this.value = value;

        boolValue = (bool)value;
        stringValue = (string)value;
        intValue = (int)value;
        floatValue = (float)value;
    }
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
            animationEvents.Add(CreateAnimationEvent(lastValue));
        }
        // create the actual event frame
        animationEvents.Add(CreateAnimationEvent(value));
    }

    public override void Apply(AnimationClip clip)
    {
        clip.events = animationEvents.ToArray();
    }

    private AnimationEvent CreateAnimationEvent(object value)
    {
        AnimationEvent e;
        e = new AnimationEvent();
        e.time = Time.time - 0.0001f;
        e.functionName = "OnEventFire";

        CustomAnimationEventMessage mediator;
        mediator = ScriptableObject.CreateInstance<CustomAnimationEventMessage>();
        mediator.valueSetter = valueSetter;
        mediator.value = value;
        e.objectReferenceParameter = mediator;

        return e;
    }
}
