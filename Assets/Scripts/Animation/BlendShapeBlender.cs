using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component handles automatically animating 
/// blendshape layers on a skinned mesh renderer each 
/// individually from 0-100 in order. 
/// 
/// There can be as many blend shapes as required 
/// and the order can be reversed. This is used for 
/// the battery indicator of the flash system.
///
/// The lerping from 0 to 100 is always happening
/// </summary>
public class BlendShapeBlender : MonoBehaviour
{
    /// <summary>The current lerped value between 0 and 100</summary>
    public float value;

    /// <summary>Updates the blend shapes from bottom to top instead of top to bottom</summary>
    public bool inverseDirection = false;

    /// <summary>Blend shapes will be set to 100 when value is 0</summary>
    public bool inverseSize = false;
    private float inverseValue
    {
        get
        {
            return 100 - value;
        }
    }
    private SkinnedMeshRenderer _skinnedMesh;
    // Start is called before the first frame update
    private void Start()
    {
        _skinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        int blendMeshCount = _skinnedMesh.sharedMesh.blendShapeCount;

        float progress = (inverseSize ? inverseValue : value) * blendMeshCount;

        for (int i = 0; i < blendMeshCount; i++)
        {
            int index = inverseDirection ? (blendMeshCount - i - 1) : i;
            float weight = Mathf.Min(progress, 100);
            _skinnedMesh.SetBlendShapeWeight(index, weight);
            progress -= weight;
        }
    }
}
