using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceMaterialIdentifier : MonoBehaviour
{
    //public bool _isGrounded = false;
    public LayerMask ground;
    public float maxCastDistance = 5f;
    
    [SerializeField] private SurfaceMaterialData _surfaceData = null;


   
    public SurfaceMaterial Cast(Ray r)
    {
        Debug.Log("A Cast called");
        if (_surfaceData != null)
        {
            Debug.Log("A SD not null");
            if (Physics.Raycast(r, out RaycastHit hitInfo, maxCastDistance, ground, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("A If statement true");

                SurfaceMaterialId id = hitInfo.transform.GetComponent<SurfaceMaterialId>();
                if (id != null)
                {
                    SurfaceMaterial material = _surfaceData.FindSurfaceMaterial(id.id);
                    if (material != null) return material;
                }
                else
                {
                    Debug.Log("A id of target null");
                }

            }
        }
        else
        {
            Debug.LogError("Surface Data not assigned", this);
        }
        return null;
    }
}

//Physics.CheckSphere(_groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);