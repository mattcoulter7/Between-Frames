using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RectTransformFollowsTransform : MonoBehaviour
{
    public Transform worldTransform;
    // Update is called once per frame
    void Update()
    {
        if (worldTransform == null) return;
        transform.position = Camera.main.WorldToScreenPoint(worldTransform.position);
    }
}
