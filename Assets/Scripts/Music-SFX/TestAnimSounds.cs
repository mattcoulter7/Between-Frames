using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestAnimSounds : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onEventEnter;
    public UnityEvent onEventExit;

    public void onTrigger()
    {
        onEventEnter.Invoke();
    }

    public void offTrigger()
    {
        onEventExit.Invoke();
    }
}
