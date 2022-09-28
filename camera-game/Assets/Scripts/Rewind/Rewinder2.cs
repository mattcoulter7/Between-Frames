using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Linq;

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

    private List<RewindInstance2> _rewindInstances = new List<RewindInstance2>();
    private Coroutine rewindCoroutine = null;

    private PlayerInput myInput;

    private void Awake()
    {
        EventDispatcher.Instance.AddEventListener("RewindConsumed", (Action<RewindInstance2>)RewindConsumed);
        EventDispatcher.Instance.AddEventListener("RegisterRewindInstance", (Action<RewindInstance2>)RegisterRewindInstance);
        EventDispatcher.Instance.AddEventListener("RemoveRewindInstance", (Action<RewindInstance2>)RemoveRewindInstance);
        
        //Controller input      
        if(myInput == null) // find inputsystem 
        {
            myInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        }

        myInput.actions["Rewind"].performed += ctx => OnRewind(); // add input context for rewind
    }

    // Start is called before the first frame update
    void Start()
    {
        if (automaticallyStartRecording)
        {
            StartRecording();
        }
    }

    private void RegisterRewindInstance(RewindInstance2 ri)
    {
        if (!_rewindInstances.Contains(ri))
        {
            ri.isRecording = isRecording;
            ri.isRewinding = isRewinding;
            _rewindInstances.Add(ri);
        }
    }

    private void RemoveRewindInstance(RewindInstance2 ri)
    {
        _rewindInstances.Remove(ri);
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

    public void OnRewind() 
    {
        RewindForSeconds();
        myInput.SwitchCurrentActionMap("UI");
    }
}
