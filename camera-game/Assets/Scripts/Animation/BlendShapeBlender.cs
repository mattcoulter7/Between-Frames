using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShapeBlender : MonoBehaviour
{
    public float value;
    public bool inverseDirection = false;
    public bool inverseSize = false;
    float inverseValue
    {
        get
        {
            return 100 - value;
        }
    }
    private SkinnedMeshRenderer _skinnedMesh;
    // Start is called before the first frame update
    void Start()
    {
        _skinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
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
