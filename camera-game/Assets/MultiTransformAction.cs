using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTransformAction : Action
{
    public Transform[] transforms;
    public bool loop = false;
    public float smoothness = 0.1f;
    public float distanceCheck = 0.01f;
    private int _nextTargetIndex = 0;
    private Transform _nextTarget {
        get {
            return (_nextTargetIndex < transforms.Length && _nextTargetIndex >= 0) ? transforms[_nextTargetIndex] : null;
        }
    }
    private void IncrementTarget(){
        _nextTargetIndex++;
        if (loop && _nextTargetIndex > transforms.Length - 1){
            _nextTargetIndex = 0;
        }
    }
    public override void WhenRunning(){
        if (_nextTarget){
            transform.position = Vector3.Lerp(transform.position, _nextTarget.position, smoothness);
            transform.rotation = Quaternion.Lerp(transform.rotation, _nextTarget.rotation, smoothness);
            transform.localScale = Vector3.Lerp(transform.localScale, _nextTarget.localScale, smoothness);

            float distance = (_nextTarget.position - transform.position).magnitude;
            if (distance < distanceCheck){
                Stop();
            }
        }
    }
    public override void OnRun(){}
    public override void OnStop(){
        IncrementTarget();
    }
}
