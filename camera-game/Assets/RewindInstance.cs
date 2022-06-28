using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.Animations;
using UnityEditor;
using System.Reflection;

public class RewindInstance : MonoBehaviour
{
    public AnimationPropertyRecord[] animationPropertyRecords;
    public int frameCount = 0;
    public UnityEvent onRewindStart;
    public UnityEvent onRewindStop;

    private Animation _animation;
    private AnimationClip _rewindAnimationClip;
    private bool _isRewinding = false;
    private float _startTime = 0f;
    private float _stopTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        foreach (AnimationPropertyRecord record in animationPropertyRecords)
        {
            record.OnInitialise(gameObject);
        }
        _animation = gameObject.AddComponent<Animation>();
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
        if (!_isRewinding)
        {
            Record();
        }
    }

    public void Record(bool optimise = true)
    {
        frameCount = 0;
        foreach (AnimationPropertyRecord record in animationPropertyRecords)
        {
            record.Record(optimise);
            frameCount += record.timeline.keys.Length;
        }
    }

    public void StartRewind(float rewindSpeed)
    {
        if (animationPropertyRecords.Length == 0) return;


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

        _animation.Play(_rewindAnimationClip.name);
        _startTime = _animation[_rewindAnimationClip.name].time;
        onRewindStart.Invoke();
    }

    public void StopRewind()
    {
        _isRewinding = false;
        _animation.Stop(_rewindAnimationClip.name);
        _stopTime = _animation[_rewindAnimationClip.name].time;
        float rewindDuration = _stopTime - _startTime;
        float cullTime = _startTime - rewindDuration;
        foreach (AnimationPropertyRecord record in animationPropertyRecords)
        {
            // clear the rewind curve
            //record._reversedAnimationCurve.keys = new Keyframe[0];

            record.timeline.keys = record.timeline.keys
                .GetWithinRange(null, cullTime)// remove cullTime frames from timeline
                .GetOffset(rewindDuration * 2);// shift timeline by rewindDuration * 2
        }
        _rewindAnimationClip.ClearCurves();
        onRewindStop.Invoke();
    }
}
