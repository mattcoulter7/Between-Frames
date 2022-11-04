using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InitialSounds : MonoBehaviour
{

    public UnityEvent onAwake;
    // Start is called before the first frame update
    void Start()
    {
        onAwake.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
