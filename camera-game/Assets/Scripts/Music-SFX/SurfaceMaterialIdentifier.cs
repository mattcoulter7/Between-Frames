using UnityEngine;
/// <summary>
/// This checks for the ground the player walks on via a raycast. It then returns a tag if there is one attached on the ground. 
/// </summary>
public class SurfaceMaterialIdentifier : MonoBehaviour
{
    //public bool _isGrounded = false;
    /// <summary>
    /// The layers in Untiy that the raycast will hit
    /// </summary>
    public LayerMask ground;

    /// <summary>
    /// The maximum cast dstance of the raycast
    /// </summary>
    public float maxCastDistance = 5f;
    
    [SerializeField] private SurfaceMaterialData _surfaceData = null;

    /// <summary>
    /// The function to cast the ray.
    /// </summary>
    /// <param name="r">This is the ray the function will cast</param>
    /// <returns>It returns a surface material that has an id attached</returns>
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
                    if (material != null)
                    {
                        return material;
                    }
                    else
                    {
                        Debug.Log("Found:" + material);
                    }

                }
                else
                {
                    Debug.Log("A id of target null");
                }

            }
        }
        else
        {
            Debug.Log("Surface Data not assigned", this);
        }
        return null;
    }
}