using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorToRectPoint : MonoBehaviour
{
    public enum AnchorMode {
        FrontLeft
    }
    public AnchorMode anchorMode;
    private RectanglePoints _rectanglePoints;
    // Start is called before the first frame update
    void Start()
    {
        _rectanglePoints = GetComponentInChildren<RectanglePoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anchorMode == AnchorMode.FrontLeft){
            transform.position = _rectanglePoints.frontLeft;
        }
    }
}
