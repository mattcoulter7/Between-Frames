using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This class is similar to PlayerKiller2,
/// But instead avoids waiting for collision events.
/// It is instead able to determine a death during the update loop
/// 
/// This is useful as systems that aim to avoid killing the player are able to perform calculations all in 1 update frame,
/// Instead of having to revert values which would result in visual flickering
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerKiller2 : MonoBehaviour
{
    public bool deadTrigger;
    public float squishTolerance;
    public float sphereCastRadius = 4f;
    public LayerMask validSquishLayers;

    public List<Vector2> directionChecks = new List<Vector2>();

    public Transform debugPoint1;
    public Transform debugPoint2;

    private Collider myCollider;
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }
    private void Update()
    {
        deadTrigger = CheckForSquish2();
    }
    // Update is called once per frame
    private bool CheckForSquish()
    {
        // check for all colliders in a sphere radius of player
        List<Collider> colliders = Physics.OverlapSphere(transform.position, squishTolerance, validSquishLayers).ToList();

        foreach (Collider col in colliders)
        {
            Vector3 fakeContactPoint = col.ClosestPoint(transform.position);

            Vector2 normal = transform.position - fakeContactPoint;
            normal.Normalize();
            debugPoint1.position = fakeContactPoint;

            RaycastHit hit;
            Physics.Raycast(transform.position, normal, out hit, squishTolerance, validSquishLayers);

            if (hit.collider != null && hit.collider != col && hit.collider.isTrigger == false)
            {
                debugPoint2.position = hit.point;
                return true;
            }
        }

        return false;
    }

    private bool CheckForSquish2()
    {
        foreach (Vector3 dir in directionChecks)
        {
            RaycastHit[] hits = DoubleRayCast(transform.position, dir, squishTolerance, validSquishLayers);

            if (hits[0].collider == null || hits[0].collider.isTrigger == true) continue;
            if (hits[1].collider == null || hits[1].collider.isTrigger == true) continue;

            return true;
        }

        return false;
    }

    private RaycastHit[] DoubleRayCast(Vector3 position,Vector3 direction,float maxDistance,LayerMask layers)
    {
        RaycastHit[] hits = new RaycastHit[2] { new RaycastHit(), new RaycastHit() };

        Physics.Raycast(position, direction, out hits[0], maxDistance, layers);
        Physics.Raycast(position, -direction, out hits[1], maxDistance, layers);

        return hits;
    }
}
