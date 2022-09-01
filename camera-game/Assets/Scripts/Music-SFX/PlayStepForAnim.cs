using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//<summary>This class is for the character to play a Step Sound via a keyframe in the walking animation.
//This is needed because the animation requires a Method attached to a script on the character to be called at the keyframe</summary>
public class PlayStepForAnim : MonoBehaviour
{
    //<summary>This is the SoundEvent that is found in every scene. This holds the method to play the steps</summary>
    public UnityEvent Step;

    //private void Awake()
    //{
    //    SoundEvent = GameObject.Find("SoundEvent");
    //}

    public void PlayStep()
    {
        Step.Invoke();
    }

}
