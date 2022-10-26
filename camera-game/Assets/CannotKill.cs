using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannotKill : MonoBehaviour
{
    public float tolerance = 1f;
    public LayerMask crushLayers;
    public bool Alive()
    {
        RaycastHit hit1;
        Physics.Raycast(transform.position, Vector3.up, out hit1, tolerance, crushLayers,QueryTriggerInteraction.Ignore);
        RaycastHit hit2;
        Physics.Raycast(transform.position, Vector3.down, out hit2, tolerance, crushLayers, QueryTriggerInteraction.Ignore);
        return (hit1.collider == null || hit2.collider == null);
    }
}
