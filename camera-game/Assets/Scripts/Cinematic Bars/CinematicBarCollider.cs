using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is responsible for updating the object layer to the Cinematic Bar Collision layer
///
/// Must be placed on the renderer object
///
/// Updates the collision
/// </summary>
public class CinematicBarCollider : MonoBehaviour
{
    /// <summary>The reference to the gameObject collider. Collider is called fetched if null.</summary>
    public Collider colliderObject;
    
    /// <summary>The index of the layer which collides with the cinematic bars</summary>
    public int targetLayer = 9;

    private int _originalLayer;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (colliderObject == null)
        {
            colliderObject = GetComponent<Collider>();
        }
        _originalLayer = colliderObject.gameObject.layer;
    }

    private void OnBecameVisible()
    {
        colliderObject.gameObject.layer = targetLayer;
    }

    private void OnBecameInvisible()
    {
        colliderObject.gameObject.layer = _originalLayer;
    }
}
