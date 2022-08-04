using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rewinder2 : MonoBehaviour
{
    public bool automaticallyStartRecording = true;
    public float rewindDuration = 4f;
    public float rewindSpeed = 1f;

    public bool isRewinding = false;
    public bool isRecording = false;

    public UnityEvent onRewindStart; // beings playing rewind
    public UnityEvent onRewindStop; // stops all the rewind
    public UnityEvent onContinue; // returns to normal play mode
    public UnityEvent onRewindConsumed; // returns to normal play mode

    public string rewindInputBind;

    private RewindInstance2[] _rewindInstances;
    private Coroutine rewindCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.AddEventListener("RewindConsumed", (Action<RewindInstance2>)RewindConsumed);
        _rewindInstances = FindObjectsOfType<RewindInstance2>();
        if (automaticallyStartRecording)
        {
            StartRecording();
        }
    }
    void RewindConsumed(RewindInstance2 rewindInstance)
    {
        if (rewindCoroutine != null)
        {
            StopCoroutine(rewindCoroutine);
        }
        StopRewind();
        onRewindConsumed.Invoke();
    }

    public void StartRecording()
    {
        isRecording = true;
        foreach (RewindInstance2 rewindInstance in _rewindInstances)
        {
            rewindInstance.isRecording = true;
        }
    }
    public void StopRecording()
    {
        isRecording = false;
        foreach (RewindInstance2 rewindInstance in _rewindInstances)
        {
            rewindInstance.isRecording = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRewinding && Input.GetButtonDown(rewindInputBind))
        {
            RewindForSeconds();
        }
    }

    public void RewindForSeconds()
    {
        rewindCoroutine = StartCoroutine(RewindCoroutine());
    }
    private IEnumerator RewindCoroutine()
    {
        StartRewind();
        yield return new WaitForSeconds(rewindDuration);
        StopRewind();
        rewindCoroutine = null;
    }

    public void StartRewind()
    {
        isRewinding = true;
        isRecording = false;
        onRewindStart.Invoke();
        foreach (RewindInstance2 rewindInstance in _rewindInstances)
        {
            if (rewindInstance.isActiveAndEnabled)
            {
                rewindInstance.StartRewind();
            }
        }
    }

    public void StopRewind()
    {
        isRewinding = false;
        onRewindStop.Invoke();
        foreach (RewindInstance2 rewindInstance in _rewindInstances)
        {
            if (rewindInstance.isActiveAndEnabled)
            {
                rewindInstance.StopRewind();
            }
        }
    }
    public void Continue()
    {
        isRecording = true;
        onContinue.Invoke();
        foreach (RewindInstance2 rewindInstance in _rewindInstances)
        {
            if (rewindInstance.isActiveAndEnabled)
            {
                rewindInstance.Continue();
            }
        }
    }
}
