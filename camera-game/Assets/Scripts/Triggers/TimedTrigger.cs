using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TimedTrigger : Trigger
{
    public float duration;
    public override void OnTriggerBegin()
    {
        base.OnTriggerBegin();
        StartCoroutine(Timer());
    }
    public override void OnTriggerEnd()
    {
        base.OnTriggerEnd();
    }
    IEnumerator Timer(){
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(duration);
        OnTriggerEnd();
    }
}
