using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TimedTrigger : Trigger
{
    public float _duration;
    public override void OnTriggerStart()
    {
        base.OnTriggerStart();
        StartCoroutine(Timer());
    }
    IEnumerator Timer(){
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(_duration);
        OnTriggerExit();
    }
}
