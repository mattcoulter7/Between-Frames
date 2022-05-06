using UnityEngine;
using System.Collections;
using System.Collections.Generic;    // Don't forget to add this if using a List.

public class CollisionList : MonoBehaviour
{

    // Declare and initialize a new List of GameObjects called currentCollisions.
    public List<Collider> currentCollisions = new List<Collider>();
    public List<Collider> GetCurrentCollisions(){
        return currentCollisions;
    }
    void OnCollisionEnter(Collision col)
    {
        // Add the GameObject collided with to the list.
        currentCollisions.Add(col.collider);
    }

    void OnCollisionExit(Collision col)
    {
        // Remove the GameObject collided with from the list.
        currentCollisions.Remove(col.collider);
    }
}