using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceMaterialIdentifier : MonoBehaviour
{
    private static readonly string layerToCastTo = "Ground";
    private static readonly float maxCastDistance = 10f;

    [SerializeField] private SurfaceMaterialData _surfaceData = null;

    public SurfaceMaterial Cast(Ray r)
    {
        if (_surfaceData != null)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(r, out hitInfo, maxCastDistance, LayerMask.GetMask(layerToCastTo), QueryTriggerInteraction.Ignore))
            {

                SurfaceMaterial id = hitInfo.transform.GetComponent<SurfaceMaterial>();
                if(id != null)      
                {
                    SurfaceMaterial material = _surfaceData.FindSurfaceMaterial(id.identity);
                    if (material != null) return material;
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