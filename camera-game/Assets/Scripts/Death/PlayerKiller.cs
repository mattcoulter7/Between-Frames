using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

/// <summary>
/// This class handles determining if a player is killed by configuring kill conditions
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerKiller : MonoBehaviour
{
    /// <summary>Unity Event for what else will happen on kill such as a death sound playing</summary>
    public UnityEvent onKill;
    public float angleTolerance = 20f;

    /// <summary>True if the player is dead</summary>
    public bool dead;

    /// <summary>
    /// Minimum distance between object to each opposite collider to trigger a kill
    /// X represents horizontal tolerance
    /// Y represents vertical tolerance
    /// </summary>
    public Vector2 squishTolerances;

    /// <summary>
    /// Layers which the object can be squished by
    /// </summary>
    public LayerMask validSquishLayers;


    /// <summary>
    /// The minimum distance the object needs to be inside of a collider by to trigger a kill
    /// </summary>
    public float penetrationTolerance;

    /// <summary>
    /// Layers which the object can be penetrating inside of can trigger a kill
    /// </summary>
    public LayerMask validPenetrateLayers;

    [System.Serializable]
    public class ConditionalPenetrateLayerMask
    {
        public LayerMask penetrateLayer;
        public LayerMask mustBeTouchingLayer;
    }
    public ConditionalPenetrateLayerMask[] conditionalPenetrateLayers;
    private List<Collider> currentCollisions = new List<Collider>();

    private Collider myCollider;

    /// <summary>Sets dead back to false</summary>
    public void Reset()
    {
        dead = false;
    }
    private void Kill()
    {
        dead = true;
        onKill.Invoke();
    }

    private void Start()
    {
        dead = false;
        myCollider = GetComponent<Collider>();
    }


    private void CheckForDeath(Collision col)
    {
        if (!enabled) return;
        CheckForSquish(col);
        CheckForPenetration(col);
        CheckForConditionalPenetration(col);
    }

    private List<Vector2> GetSquishDirections(Vector2 contactNormal)
    {
        List<Vector2> squishDirections = new();

        contactNormal = contactNormal.normalized;
        squishDirections.Add(contactNormal * squishTolerances);

        if (angleTolerance != 0)
        {
            squishDirections.Add(contactNormal.Rotate(angleTolerance) * squishTolerances);
            squishDirections.Add(contactNormal.Rotate(-angleTolerance) * squishTolerances);
        }

        return squishDirections;
    }

    private void CheckForSquish(Collision col)
    {
        if (dead) return;
        if (!validSquishLayers.HasLayer(col.collider.gameObject.layer)) return;

        ContactPoint contact = col.GetContact(0);
        List<Vector2> squishDirections = GetSquishDirections(contact.normal);

        foreach (Vector2 squishDirection in squishDirections)
        {
            RaycastHit hit;
            Physics.Raycast(contact.point, squishDirection, out hit, squishDirection.magnitude, validSquishLayers);

            if (hit.collider == null || hit.collider == col.collider || hit.collider.isTrigger)
                return;
        }

        Kill();
    }

    private void CheckForPenetration(Collision col)
    {
        if (dead) return;
        if (!validPenetrateLayers.HasLayer(col.collider.gameObject.layer)) return;

        Vector3 direction;
        float distance;

        Physics.ComputePenetration(
            col.collider,
            col.collider.transform.position,
            col.collider.transform.rotation,

            myCollider,
            myCollider.transform.position,
            myCollider.transform.rotation,

            out direction,
            out distance
        );
        if (distance > penetrationTolerance)
        {
            Kill();
        }
    }
    private void CheckForConditionalPenetration(Collision col)
    {
        if (dead) return;

        LayerMask currentCollisionsLayerMask = CurrentCollisionsLayerMask();
        
        foreach (ConditionalPenetrateLayerMask condition in conditionalPenetrateLayers)
        {
            if (!currentCollisionsLayerMask.HasLayer(condition.mustBeTouchingLayer)) continue;
            if (!currentCollisionsLayerMask.HasLayer(col.gameObject.layer)) continue;

            Vector3 direction;
            float distance;

            Physics.ComputePenetration(
                col.collider,
                col.collider.transform.position,
                col.collider.transform.rotation,

                myCollider,
                myCollider.transform.position,
                myCollider.transform.rotation,

                out direction,
                out distance
            );
            if (distance > penetrationTolerance)
            {
                Kill();
                return;
            }
        }
    }
    
    private LayerMask CurrentCollisionsLayerMask()
    {
        List<int> currentCollisionLayers = new List<int>();
        foreach (Collider col in currentCollisions)
        {
            if (col == null) continue;
            try
            {
                if (!currentCollisionLayers.Contains(col.gameObject.layer))
                {
                    currentCollisionLayers.Add(col.gameObject.layer);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        string[] currentCollisionLayerNames = currentCollisionLayers.Select(l => LayerMask.LayerToName(l)).ToArray();
        return LayerMask.GetMask(currentCollisionLayerNames);
    }
    private void OnCollisionEnter(Collision col)
    {
        currentCollisions.Add(col.collider);
        CheckForDeath(col);
    }

    private void OnCollisionStay(Collision col)
    {
        CheckForDeath(col);
    }

    private void OnCollisionExit(Collision col)
    {
        currentCollisions.Remove(col.collider);
    }
}
