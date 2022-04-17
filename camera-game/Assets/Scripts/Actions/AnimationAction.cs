using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAction : Action
{
    public string animationName = "";
    public override void OnRun()
    {
        GetComponent<Animator>().Play(animationName);
    }
    public override void WhenRunning()
    {
    }
    public override void OnStop()
    {

    }
}
