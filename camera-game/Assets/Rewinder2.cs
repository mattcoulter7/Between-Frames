using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder2 : MonoBehaviour
{
    public float rewindSpeed = 2f;
    private RewindInstance[] _rewindInstances;
    // Start is called before the first frame update
    void Start()
    {
        _rewindInstances = FindObjectsOfType<RewindInstance>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartRewind();
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        foreach (RewindInstance rewindInstance in _rewindInstances)
        {
            rewindInstance.StartRewind(rewindSpeed);
        }
    }

    public void StopRewind()
    {
        foreach (RewindInstance rewindInstance in _rewindInstances)
        {
            rewindInstance.StopRewind();
        }
    }
}
