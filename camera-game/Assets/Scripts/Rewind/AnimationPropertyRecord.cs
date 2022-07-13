using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// OBSERVES FLOAT VALUES
[System.Serializable]
public class AnimationPropertyRecord : AnimationRecord
{
    public string animationProperty;
    public AnimationCurve timeline = new AnimationCurve(); // holds ongoing history of animation property change
    public AnimationCurve rewind = new AnimationCurve(); // holds reversed instance of timeline until rewind stops

    private string[] _unityPropertyArray;
    private float changeTolerance = 0.001f; // change needs to be greater than this for a keyframe to be recorded

    public override object lastValue => timeline.length > 0 ? timeline[timeline.length - 1].value : null;
    public override int frameCount => timeline.length;

    // debug properties
    private float _timelineStartTime;
    private float _timelineEndTime;
    private float _timelineDuration;
    private float _rewindStartTime;
    private float _rewindEndTime;
    private float _rewindDuration;

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
