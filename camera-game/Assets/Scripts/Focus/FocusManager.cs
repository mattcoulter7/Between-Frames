using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FocusManager : MonoBehaviour
{
    public float focusDistanceMax;
    public float focusDistanceMin;
    public float currentFocus;
    public PostProcessLayer postProcessLayer;

    List<PostProcessVolume> volList = new List<PostProcessVolume>();
    DepthOfField dph;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessManager.instance.GetActiveVolumes(postProcessLayer, volList, true, true);
        //
        foreach (PostProcessVolume vol in volList)
        {
            PostProcessProfile ppp = vol.profile;
            if (ppp)
            {
                ppp.TryGetSettings<DepthOfField>(out dph);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        dph.focusDistance.value = currentFocus;
    }
}
