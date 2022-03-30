using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform targetTransform;
    public float smoothness = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(targetTransform.position,targetTransform.position,smoothness);
        transform.rotation = Quaternion.Lerp(transform.rotation,targetTransform.rotation,smoothness);
        transform.localScale = Vector3.Lerp(transform.localScale,targetTransform.localScale,smoothness);
    }
}
