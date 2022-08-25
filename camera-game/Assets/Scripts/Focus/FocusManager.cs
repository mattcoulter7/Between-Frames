using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// This class handles updating the post processing effects module Depth of field value
/// Since the values are global in post processing module, having a script to control it for each game instance is necessary
/// </summary>
public class FocusManager : MonoBehaviour
{
    /// <summary>
    /// The maximum focus amount
    /// </summary>
    public float focusDistanceMax;

    /// <summary>
    /// The minimum focus amount
    /// </summary>
    public float focusDistanceMin;

    /// <summary>
    /// The current focus amount
    /// </summary>
    public float currentFocus;

    /// <summary>
    /// Reference to the post processing layer so that the focus variable can be applied
    /// </summary>
    public PostProcessLayer postProcessLayer;

    private List<PostProcessVolume> volList = new List<PostProcessVolume>();
    private DepthOfField dph;

    // Start is called before the first frame update
    private void Start()
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
    private void Update()
    {
        dph.focusDistance.value = currentFocus;
    }
}
