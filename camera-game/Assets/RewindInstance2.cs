using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewindInstance2 : MonoBehaviour
{
    [System.Serializable]
    public class RewindProperty {
        public DynamicModifier getter;
        public DynamicModifier setter;
    }
    [System.Serializable]
    public class Frame {
        public object value;
        public RewindProperty property;
    }
    public class FrameCollection {
        public List<Frame> frames = new List<Frame> { };
    }
    public bool isRewinding = false;
    public bool isRecording = false;
    public List<RewindProperty> rewindProperties = new List<RewindProperty> { };
    public List<FrameCollection> frames = new List<FrameCollection> { };

    public UnityEvent onRewindStart;
    public UnityEvent onRewindStop;
    public UnityEvent onContinue;

    public void StartRewind()
    {
        onRewindStart.Invoke();
        isRewinding = true;
        isRecording = false;
    }
    public void StopRewind()
    {
        onRewindStop.Invoke();
        isRewinding = false;
    }
    public void Continue()
    {
        onContinue.Invoke();
        isRecording = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        foreach (RewindProperty prop in rewindProperties)
        {
            prop.getter.OnInitialise();
            prop.setter.OnInitialise();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else if (isRecording)
        {
            Record();
        }
    }

    private void Record()
    {
        FrameCollection frameCollection = new FrameCollection();
        foreach (RewindProperty prop in rewindProperties)
        {
            Frame frame = new Frame();
            frame.value = prop.getter.GetValue();
            frame.property = prop;
            frameCollection.frames.Add(frame);
        }
        frames.Insert(0,frameCollection);
    }

    private void Rewind()
    {
        if (frames.Count == 0)
        {
            EventDispatcher.Instance.Dispatch("RewindConsumed", this);
            return;
        }

        FrameCollection targetFrame = frames[0];
        foreach (Frame frame in targetFrame.frames)
        {
            frame.property.setter.SetValue(frame.value);
        }
        frames.RemoveAt(0);
    }
}