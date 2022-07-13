using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.Animations;
using UnityEditor;
using System.Reflection;
using System;

public class RewindInstance : MonoBehaviour
{
    public AnimationPropertyRecord[] animationPropertyRecords;
    public AnimationEventRecord[] animationEventRecords;
    public UnityEvent onRewindStart;
    public UnityEvent onRewindStop;

    private AnimationRecord[] _animationRecords; // abstracted array containing both properties & events
    private Animation _animation;
    private AnimationClip _rewindAnimationClip;
    private bool _isRewinding = false;
    private float _startTime = 0f;
    private float _stopTime = 0f;

    [Header("Debug Properties")]
    private int _frameCount = 0;
    private float _rewindClipActualDuration;
    private float _rewindClipCalculatedDuration;

    
    // Start is called before the first frame update
    void Start()
    {
        AbstractAnimationRecords();

        foreach (AnimationRecord record in _animationRecords)
        {
            record.OnInitialise();
        }
        _animation = gameObject.AddComponent<Animation>();
        _animation.playAutomatically = false;

        _rewindAnimationClip = new AnimationClip();
        _rewindAnimationClip.name = "Rewind Segment (Instance)";
        _rewindAnimationClip.legacy = true;

        // now animate the GameObject
        _animation.AddClip(_rewindAnimationClip, _rewindAnimationClip.name);

        Record(false); // record an initial key frame to mark the start of the clip
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRewinding)
        {
            _rewindClipActualDuration = _animation[_rewindAnimationClip.name].time;
        }
        else
        {
            Record(true);
        }

        foreach (AnimationPropertyRecord record in animationPropertyRecords)
        {
            record.Update();
        }
    }

    public void Record(bool optimise = true)
    {
        _frameCount = 0;
        foreach (AnimationRecord record in _animationRecords)
        {
            record.Record(optimise);
            _frameCount += record.frameCount;
        }
    }

    public void StartRewind(float rewindSpeed)
    {
        if (animationPropertyRecords.Length == 0 && animationEventRecords.Length == 0) return;

        Record(false); // record an end key frame to mark the beginning of the rewind
        _isRewinding = true;

        foreach (AnimationPropertyRecord record in animationPropertyRecords)
        {
            // reverse the keys
            record.rewind.keys = record.timeline.keys.GetReversed();

            // make key time start at 0 so the animation plays instantly
            Keyframe earliestKey = record.timeline.keys.GetEarliest();
            record.rewind.keys = record.rewind.keys
                .GetOffset(-earliestKey.time)
                .GetScaled(1 / rewindSpeed);

            // apply the curve to the clip
            record.Apply(_rewindAnimationClip);
        }

        foreach (AnimationEventRecord record in animationEventRecords)
        {
            // reverse the keys
            AnimationEvent[] animationEvents = record.animationEvents.ToArray();
            AnimationEvent[] reversedEvents = animationEvents.GetReversed();

            // make key time start at 0 so the animation plays instantly
            AnimationEvent earliestKey = animationEvents.GetEarliest();
            reversedEvents = reversedEvents
                .GetOffset(-earliestKey.time)
                .GetScaled(1 / rewindSpeed);

            _rewindAnimationClip.events = reversedEvents;
            // apply the curve to the clip
            //record.Apply(_rewindAnimationClip);
        }

        _animation.Play(_rewindAnimationClip.name);
        _startTime = Time.time;
        onRewindStart.Invoke();
    }

    public void StopRewind()
    {
        _isRewinding = false;
        _stopTime = Time.time;
        _animation.Stop(_rewindAnimationClip.name);
        float rewindDuration = _stopTime - _startTime;
        foreach (AnimationPropertyRecord record in animationPropertyRecords)
        {
            float duration = record.timeline.keys.GetLatest().time;
            float cullTime = duration - rewindDuration;
            record.timeline.keys = record.timeline.keys.GetWithinRange(null, cullTime); // remove cullTime frames from timeline
            record.timeline.keys = record.timeline.keys.GetOffset(Time.time - cullTime);  // shift timeline by rewindDuration * 2
        }
        foreach (AnimationEventRecord record in animationEventRecords)
        {
            float duration = record.animationEvents.ToArray().GetLatest().time;
            float cullTime = duration - rewindDuration;
            record.animationEvents = record.animationEvents.ToArray().GetWithinRange(null, cullTime).ToList(); // remove cullTime frames from timeline
            record.animationEvents = record.animationEvents.ToArray().GetOffset(Time.time - cullTime).ToList();  // shift timeline by rewindDuration * 2
        }
        _rewindAnimationClip.ClearCurves();
        _rewindAnimationClip.events = new AnimationEvent[] { };
        Record(false);
        onRewindStop.Invoke();
    }

    private void AbstractAnimationRecords()
    {
        List<AnimationRecord> animationRecords = new List<AnimationRecord>();
        animationRecords.AddRange(animationPropertyRecords);
        animationRecords.AddRange(animationEventRecords);
        _animationRecords = animationRecords.ToArray();
    }

    // hacky intermediate function for call methods with a boolean parameter
    public void OnEventFire(CustomAnimationEventMessage mediator)
    {
        mediator.valueSetter.SetValue(mediator.value);
    }
}
