using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackbar : BoneWeightedBoxController
{
    public enum ViewPortAnchorMode {
        BOTTOM,
        TOP
    }
    public ViewPortAnchorMode viewportAnchor = ViewPortAnchorMode.TOP;
    float moveSpeed = 0.1f;
    public float distance = 10f;
    public float leftHeightOffset = 0.2f; // the left height in viewport units
    public float rightHeightOffset = 0.2f; // the right height in viewport units
    void Start(){
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // calculate top rays
        Ray topLeftRay = Camera.main.ViewportPointToRay(new Vector3(0,(float)viewportAnchor,0));
        Ray topRightRay = Camera.main.ViewportPointToRay(new Vector3(1,(float)viewportAnchor,0));

        // calculate top positions
        Vector3 targetTopLeftFront = topLeftRay.GetPoint(distance);
        Vector3 targetTopLeftBack = topLeftRay.GetPoint(distance + 5);
        Vector3 targetTopRightFront = topRightRay.GetPoint(distance);
        Vector3 targetTopRightBack = topRightRay.GetPoint(distance + 5);

        // calculate bottom rays
        float adjustedHeightLeft = (float)viewportAnchor - leftHeightOffset * moveSpeed;
        float adjustedHeightRight = (float)viewportAnchor - rightHeightOffset * moveSpeed;
        if (viewportAnchor == ViewPortAnchorMode.BOTTOM){
            adjustedHeightLeft *= -1;
            adjustedHeightRight *= -1;
        }
        Ray bottomLeftRay = Camera.main.ViewportPointToRay(new Vector3(0,adjustedHeightLeft,0));
        Ray bottomRightRay = Camera.main.ViewportPointToRay(new Vector3(1,adjustedHeightRight,0));

        // calculate bottom positions
        Vector3 targetBottomLeftFront = bottomLeftRay.GetPoint(distance);
        Vector3 targetBottomLeftBack = bottomLeftRay.GetPoint(distance + 5);
        Vector3 targetBottomRightFront = bottomRightRay.GetPoint(distance);
        Vector3 targetBottomRightBack = bottomRightRay.GetPoint(distance + 5);

        // bottom should be flat so objects aren't pushed off of platform
        targetBottomLeftBack.y = targetBottomLeftFront.y;
        targetBottomRightBack.y = targetBottomRightFront.y;

        // recalculate origin
        Ray middleRay = Camera.main.ViewportPointToRay(new Vector3(0.5f,0,0));
        Vector3 newOrigin = middleRay.GetPoint(distance);
        newOrigin.y = (targetTopLeftFront - targetBottomLeftBack).y;
        
        // set new origin (ensures object is still rendered)
        transform.position = newOrigin;

        // set top positions
        topLeftFront.position = targetTopLeftFront;
        topLeftBack.position = targetTopLeftBack;
        topRightFront.position = targetTopRightFront;
        topRightBack.position = targetTopRightBack;

        // set bottom positions
        bottomLeftFront.position = targetBottomLeftFront;
        bottomLeftBack.position = targetBottomLeftBack;
        bottomRightFront.position = targetBottomRightFront;
        bottomRightBack.position = targetBottomRightBack;
    }
}

