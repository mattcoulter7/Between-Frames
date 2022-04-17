using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform targetTransform;
    public bool followPosition = true;
    public bool localPosition = false;
    public bool followRotation = true;
    public bool localRotation = false;
    public bool followScale = true;

    // Update is called once per frame
    void Update()
    {
        if (followPosition){
            if (localPosition){
                transform.localPosition = targetTransform.position - transform.parent.position; 
            } else {
                transform.position = targetTransform.position;
            }
        }
        if (followRotation){
            if (localRotation){
                transform.localRotation = targetTransform.localRotation;
            } else {
                transform.rotation = targetTransform.rotation;
            }
        } 
        if (followScale) transform.localScale = targetTransform.localScale;
    }
}
