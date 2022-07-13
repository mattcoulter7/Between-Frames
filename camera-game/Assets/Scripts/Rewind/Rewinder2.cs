using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rewinder2 : MonoBehaviour
{
    public float rewindDuration = 4f;
    public float rewindSpeed = 1f;

    public bool isRewinding = false;
    public bool isRecording = false;

    public UnityEvent onRewindStart; // beings playing rewind
    public UnityEvent onRewindStop; // stops all the rewind
    public UnityEvent onContinue; // returns to normal play mode

    private RewindInstance2[] _rewindInstances;
    // Start is called before the first frame update
    void Start()
    {
        _rewindInstances = FindObjectsOfType<RewindInstance2>();
        isRecording = true;
        foreach (RewindInstance2 rewindInstance in _rewindInstances)
        {
            rewindInstance.isRecording = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRewinding && Input.GetKeyDown(KeyCode.K))
        {
            RewindForSeconds();
        }

        if (!isRecording && Input.GetKeyDown(KeyCode.Return))
        {
            Continue();
        }
    }

    public void RewindForSeconds()
    {
        StartCoroutine(RewindCoroutine());
    }
    private IEnumerator RewindCoroutine()
    {
        StartRewind();
        yield return new WaitForSeconds(rewindDuration);
        StopRewind();
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
