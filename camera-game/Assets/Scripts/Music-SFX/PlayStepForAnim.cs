using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//<summary>This class is for the character to play a Step Sound via a keyframe in the walking animation.
//This is needed because the animation requires a Method attached to a script on the character to be called at the keyframe</summary>
[RequireComponent(typeof(SurfaceMaterialIdentifier))]
public class PlayStepForAnim : MonoBehaviour
{
   // private Rigidbody _rb;
    private Transform _groundChecker;

    //<summary>This is the SoundEvent that\ is found in every scene. This holds the method to play the steps</summary>
    public UnityEvent Step;
    Ray r;
    private void Awake()
    {
       // _rb = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
         r = new(_groundChecker.position + Vector3.up, Vector3.down); //Ray
    }

    public void PlayStep()
    {
        //Debug.Log("PlayStep called");
        
        SurfaceMaterial castResult = GetComponent<SurfaceMaterialIdentifier>().Cast(r);
        Debug.DrawRay(transform.position, r.direction, Color.red, 1f);

        if(castResult == null)
        {
            Debug.Log("Cast Result null");
            
        }
        AM.Instance.PlayFootsteps(castResult);
    }
}
