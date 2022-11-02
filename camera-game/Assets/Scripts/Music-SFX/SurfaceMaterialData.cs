using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gives the player knowledge of what tags there are, and provides a check accordingly. 
/// </summary>

//[CreateAssetMenu(fileName = "SurfaceMaterialData", menuName = "Surface/Material/Data", order = 100)]
public class SurfaceMaterialData : MonoBehaviour
{
    [SerializeField]
    private List<SurfaceMaterial> surfaceMaterials = new();

    /// <summary>
    /// Searches for a provided tag within the enum. If theres no tag of that name, or the tag does not exist, it will return null
    /// </summary>
    /// <param name="identity">This is the provided tag given by the material the player walks on</param>
    /// <returns></returns>
    public SurfaceMaterial FindSurfaceMaterial(SurfaceIdentity identity)
    {
        foreach (SurfaceMaterial s in surfaceMaterials)
        {
            if (s.identity.Equals(identity)) return s;
          
        }
        return null;
    }
}
