using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// OBSERVES FLOAT VALUES
[System.Serializable]
public class AnimationPropertyRecord
{
    public string animationProperty;
    public string baseType;
    public string unityProperty;
    public AnimationCurve timeline; // holds ongoing history of animation property change
    private float timelineStartTime;
    private float timelineEndTime;
    private float timelineDuration;
    public AnimationCurve rewind; // holds reversed instance of timeline until rewind stops
    private float rewindStartTime;
    private float rewindEndTime;
    private float rewindDuration;

    private string[] _unityPropertyArray;
    private float changeTolerance = 0.001f; // change needs to be greater than this for a keyframe to be recorded
    private object _parent;
    private System.Type _type;
    private float? _lastValue
    {
        get
        {
            return timeline.keys.Length > 0 ? timeline.keys[timeline.keys.Length - 1].value : null;
        }
    }
    public void OnInitialise(GameObject gameObject)
    {
        timeline = new AnimationCurve();
        rewind = new AnimationCurve();

        _unityPropertyArray = unityProperty.Split(".");
        _parent = gameObject.GetComponent(baseType); // hold references to base object which holds the variable
        _type = _parent.GetType(); // store the actual type object
    }
    public object GetPropertyValueDynamically()
    {
        //bool valid = AnimationUtility.GetFloatValue(_gameObject, _binding, out result);
        object parent = _parent;
        System.Type type = _type;
        PropertyInfo property;
        FieldInfo field;
        foreach (string dir in _unityPropertyArray)
        {
            property = type.GetProperty(dir);
            field = type.GetField(dir);
            type = (property != null ? property.PropertyType : (field != null ? field.FieldType : null));
            parent = (property != null ? property.GetValue(parent, null) : (field != null ? field.GetValue(parent) : null));
        }

        return parent;
    }
    public void Record(bool optimise = true)
    {
        float value = (float)GetPropertyValueDynamically();
        if (optimise)
        {
            // OPTIMISATION 1: Only store keyframes if the data has actually changed\
            if (!_lastValue.HasValue || (Mathf.Abs(value - _lastValue.Value) >= changeTolerance))
            {
                timeline.AddKey(new Keyframe(Time.time, value));
            }
        }
        else
        {
            timeline.AddKey(new Keyframe(Time.time, value));
        }
    }

    public void Apply(AnimationClip clip)
    {
        clip.SetCurve("", _type, animationProperty, rewind);
    }

    public void Update()
    {
        timelineStartTime = timeline.keys.Length > 0 ? timeline.keys.GetEarliest().time : 0;
        timelineEndTime = timeline.keys.Length > 0 ? timeline.keys.GetLatest().time : 0;
        timelineDuration = timelineEndTime - timelineStartTime;

        rewindStartTime = rewind.keys.Length > 0 ? rewind.keys.GetEarliest().time : 0;
        rewindEndTime = rewind.keys.Length > 0 ? rewind.keys.GetLatest().time : 0;
        rewindDuration = rewindEndTime - rewindStartTime;
    }
}
