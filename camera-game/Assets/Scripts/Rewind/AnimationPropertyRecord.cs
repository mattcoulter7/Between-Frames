using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Observes float values for realtime recording of of an Animation Component
/// </summary>
[System.Serializable]
public class AnimationPropertyRecord : AnimationRecord
{
    /// <summary>
    /// The name of the property, i.e. position.x
    /// This property name must line up with Unity's Aniamtion property naming convention
    /// </summary>
    public string animationProperty;

    /// <summary>
    /// Ths timeline holds the keyframes from start of the level to current time
    /// As rewind occur, keyframes are extracted from timeline and put into rewind
    /// </summary>
    public AnimationCurve timeline = new AnimationCurve();

    /// <summary>
    /// Holds reversed instance of timeline until rewind stops then the keyframes are cleared
    /// </summary>
    public AnimationCurve rewind = new AnimationCurve();

    /// <summary>
    /// The last value from the keyframe stored
    /// </summary>
    public override object lastValue => timeline.length > 0 ? timeline[timeline.length - 1].value : null;

    /// <summary>
    /// The amount of keyframes the have been saved in the timeline
    /// </summary>
    public override int frameCount => timeline.length;

    private string[] _unityPropertyArray;
    private float changeTolerance = 0.001f; // change needs to be greater than this for a keyframe to be recorded


    // debug properties
    private float _timelineStartTime;
    private float _timelineEndTime;
    private float _timelineDuration;
    private float _rewindStartTime;
    private float _rewindEndTime;
    private float _rewindDuration;

    /// <summary>
    /// Record a single keyframe of the configured aniamtionproperty value into the timeline
    /// </summary>
    /// <param name="optimise">Enabled this option will only record keyframes when the value changes, as opposed to every single frame</param>
    public override void Record(bool optimise = true)
    {
        float value = (float)valueGetter.GetValue();
        if (optimise)
        {
            // OPTIMISATION 1: Only store keyframes if the data has actually changed\
            if (lastValue == null || (Mathf.Abs(value - (float)lastValue) >= changeTolerance))
            {
                timeline.AddKey(new Keyframe(Time.time, value));
            }
        }
        else
        {
            timeline.AddKey(new Keyframe(Time.time, value));
        }
    }

    /// <summary>
    /// Saves the rewind animation
    /// </summary>
    /// <param name="clip"></param>
    public override void Apply(AnimationClip clip)
    {
        System.Type type = valueGetter.objectReference.GetType();
        clip.SetCurve("", type, animationProperty, rewind);
    }

    public void Update()
    {
        _timelineStartTime = timeline.keys.Length > 0 ? timeline.keys.GetEarliest().time : 0;
        _timelineEndTime = timeline.keys.Length > 0 ? timeline.keys.GetLatest().time : 0;
        _timelineDuration = _timelineEndTime - _timelineStartTime;

        _rewindStartTime = rewind.keys.Length > 0 ? rewind.keys.GetEarliest().time : 0;
        _rewindEndTime = rewind.keys.Length > 0 ? rewind.keys.GetLatest().time : 0;
        _rewindDuration = _rewindEndTime - _rewindStartTime;
    }
}
