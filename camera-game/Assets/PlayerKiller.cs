using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    CollisionList collisionList;
    void Start(){
        collisionList = GetComponent<CollisionList>();
    }
    bool IsWithin(Collider c, Vector3 point, bool useRigidbody)
    {
        Vector3 closest = c.ClosestPoint(point);
        Vector3 origin = c.transform.position + (c.transform.rotation * c.bounds.center);
        Vector3 originToContact = closest - origin;
        Vector3 pointToContact = closest - point;

        // If you're checking if a point is within a moving rigidbody and want to use it instead (ideally a single collider rigidbody; multiple could move the center of mass between the colliders, placing it "outside" and returning false positives). 
        Rigidbody r = c.attachedRigidbody;
        if (useRigidbody && (r != null))
        {
            // The benefit of this is the use of the center of mass for a more accurate physics origin; we multiply by rotation to convert it from it's local-space to a world offset.
            originToContact = closest - (r.position + (r.rotation * r.centerOfMass));
        }
        // Here we make the magic, originToContact points from the center to the closest point. So if the angle between it and the pointToContact is less than 90, pointToContact is also looking from the inside-out.
        // The angle will probably be 180 or 0, but it's bad to compare exact floats and the rigidbody centerOfMass calculation could add some potential wiggle to the angle, so we use "< 90" to account for any of that.
        return (Vector3.Angle(originToContact, pointToContact) < 90);
    }
    void OnCollisionStay(Collision other)
    {
        List<Collider> currentCollisions = collisionList.GetCurrentCollisions();
        Debug.Log(currentCollisions.Count);
    }
}
