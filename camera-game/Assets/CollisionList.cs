using UnityEngine;
using System.Collections;
using System.Collections.Generic;    // Don't forget to add this if using a List.

public class CollisionList : MonoBehaviour
{

    // Declare and initialize a new List of GameObjects called currentCollisions.
    //public List<Collider> currentCollisions = new List<Collider>();
    public Dictionary<Collider,bool> currentCollisions = new Dictionary<Collider, bool>();
    public List<Collider> GetCurrentCollisions(){
        List<Collider> collisions = new List<Collider>();
        foreach (KeyValuePair<Collider, bool> col in currentCollisions){
            if (col.Value){
                collisions.Add(col.Key);
            }
        }
        return collisions;
    }
    void OnCollisionStay(Collision col)
    {
        // Add the GameObject collided with to the list.
        currentCollisions[col.collider] = true;
    }
    void OnCollisionEnter(Collision col)
    {
        // Add the GameObject collided with to the list.
        currentCollisions[col.collider] = true;
    }

    void OnCollisionExit(Collision col)
    {
        // Remove the GameObject collided with from the list.
        currentCollisions[col.collider] = false;
    }
}