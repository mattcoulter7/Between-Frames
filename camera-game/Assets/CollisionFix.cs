using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFix : MonoBehaviour
{
    public LayerMask layers;
    public bool ignoreVerticalChecks = true;
    Vector3 lastPosition = Vector3.zero;
    void FixedUpdate()
    {
        if (lastPosition == Vector3.zero) lastPosition = transform.position;
        
        Vector3 direction = transform.position - lastPosition;
        if (ignoreVerticalChecks) direction.y = 0;
         Ray ray = new Ray(lastPosition, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit,direction.magnitude, layers);
        if (hit.collider && !hit.collider.isTrigger)
        {
            transform.position = lastPosition;
        }

        this.lastPosition = transform.position;
    }
}
