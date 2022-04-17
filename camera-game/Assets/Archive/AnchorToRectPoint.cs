using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorToRectPoint : MonoBehaviour
{
    public enum AnchorMode {
        FrontLeft
    }
    public AnchorMode anchorMode;
    public RectanglePoints _rectanglePoints;

    // Update is called once per frame
    void Update()
    {
        if (anchorMode == AnchorMode.FrontLeft){
            transform.position = _rectanglePoints.frontLeft;
        }
    }
}
