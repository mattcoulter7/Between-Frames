using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAction : Action
{
    public Transform transformations;
    public float smoothness = 0.2f;

    public override void OnRun()
    {
    }
    public override void WhenRunning()
    {
        transform.position = Vector3.Lerp(transform.position, transformations.position, smoothness);
        transform.rotation = Quaternion.Lerp(transform.rotation, transformations.rotation, smoothness);
        transform.localScale = Vector3.Lerp(transform.localScale, transformations.localScale, smoothness);

        if (transform.position.Equals(transformations.position) &&
            transform.rotation.Equals(transformations.rotation) &&
            transform.localScale.Equals(transformations.localScale))
        {
            Stop();
        }
    }
    public override void OnStop()
    {
        Debug.Log("Finished");
    }
}
