using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder2 : MonoBehaviour
{
    private RewindInstance[] _rewindInstances;
    // Start is called before the first frame update
    void Start()
    {
        _rewindInstances = MonoBehaviour.FindObjectsOfType<RewindInstance>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginRewind()
    {

    }

    public void StopRewind()
    {

    }
}
