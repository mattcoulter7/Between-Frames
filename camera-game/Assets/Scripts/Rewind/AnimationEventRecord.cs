using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


/// <summary>
/// This class is similar to the Animation Record, but instead of recording only float values, 
/// can set values of other types, and trigger function calls
/// </summary>
[System.Serializable]
public class AnimationEventRecord : AnimationRecord
{
    /// <summary>
    /// Used for setting the value property
    /// </summary>
    public DynamicModifier valueSetter;

    /// <summary>
    /// function called to update variable
    /// </summary>
    public string functionName;

    /// <summary>
    /// List of AnimationEvents used for storing the AnimationEvent history
    /// </summary>
    public List<AnimationEvent> animationEvents;

    /// <summary>
    /// Returns the last value saved into the aniamtion event history, or null if the history is empty
    /// </summary>
    public override object lastValue => animationEvents.Count > 0 ? ((CustomAnimationEventMessage)(animationEvents[animationEvents.Count - 1].objectReferenceParameter)).value : null;

    /// <summary>
    /// The number of animation events that have been saved
    /// </summary>
    public override int frameCount => animationEvents.Count;

    /// <summary>
    /// Ensures all of the DynamicModifiers are ready to be used
    /// </summary>
    public override void OnInitialise()
    {
        base.OnInitialise();
        valueSetter.OnInitialise();
    }

    /// <summary>
    /// Records a single AnimationEvent keyframe
    /// </summary>
    /// <param name="optimise">true will only save the keyframe if the value has changed</param>
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

    /// <summary>
    /// Saves the cached animationEvents into the AnimationClip
    /// </summary>
    /// <param name="clip">the AnimationClip in which the cache will be saved to</param>
    public override void Apply(AnimationClip clip)
    {
        clip.events = animationEvents.ToArray();
    }

    /// <summary>
    /// Generates a prepared AnimationEvent which is already linked with the OnEventFire callback
    /// OnEventFire will handle calling the custom setter DynamicObject
    /// </summary>
    /// <param name="value">The value passed into the mediator DynamicObject which will be set</param>
    /// <param name="timeOffset">An offset from the current time, used for saving a keyframe right before the actual keyframe to avoid weird interpolation</param>
    /// <returns>The AnimationEvent</returns>
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
