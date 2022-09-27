using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "SurfaceMaterialData", menuName = "Surface/Material/Data", order = 100)]
public class SurfaceMaterialData : MonoBehaviour
{
    [SerializeField]
    private List<SurfaceMaterial> surfaceMaterials = new();

    public SurfaceMaterial FindSurfaceMaterial(SurfaceIdentity identity)
    {
        foreach (SurfaceMaterial s in surfaceMaterials)
        {
            if (s.identity.Equals(identity)) return s;
          
        }
        return null;
    }
}
