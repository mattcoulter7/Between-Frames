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

    void Reposition()
    {
        if (anchorMode == AnchorMode.FrontLeft)
        {
            transform.position = _rectanglePoints.frontLeft;
        }
    }

    private void Start()
    {
        Reposition();
    }

    // Update is called once per frame
    void Update()
    {
        Reposition();
    }
}
