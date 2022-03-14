using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    public Transform target;
    public float lookSmoothness = 0.25f; // camera rotating
    public float trackSmoothness = 0.25f; // track camera moving
    public Vector3 offset = new Vector3(0, 0, 0);
    public Vector3 lookOffset = new Vector3(0,0,0);

    // Update is called once per frame
    void FixedUpdate()
    {
        // updated camera position
        Vector3 localOffset = Quaternion.Euler(
            0, // ignore x-axis rotations
            target.rotation.eulerAngles.y,
            target.rotation.eulerAngles.z
        ) * offset;
        Vector3 destination = target.position + localOffset;
        transform.position = Vector3.Lerp(transform.position, destination, trackSmoothness);

        // update camera angle
        Vector3 lookDestination = target.TransformPoint(lookOffset);
        lookDestination.y = target.position.y + lookOffset.y; // don't want target angle to affect this value

        Vector3 toLookDestination = lookDestination - transform.position;

        Quaternion lookOnLook = Quaternion.LookRotation(toLookDestination);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, lookSmoothness);
    }
}
