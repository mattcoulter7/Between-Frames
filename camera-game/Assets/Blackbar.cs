using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackbar : BoneWeightedBoxController
{
    public float distance
    {
        get
        {
            return 10f;
        }
    }
    public float barWidth = 90f;
    public float rotation = 90f;
    public float leftAngleDepth = 20f; // the left height in viewport units
    public float rightAngleDepth = 20f; // the right height in viewport units
    float topLeftAngle
    {
        get
        {
            return rotation + barWidth / 2;
        }
    }
    float topRightAngle
    {
        get
        {
            return rotation - barWidth / 2;
        }
    }
    float bottomLeftAngle
    {
        get
        {
            return topLeftAngle + leftAngleDepth;
        }
    }
    float bottomRightAngle
    {
        get
        {
            return topRightAngle - rightAngleDepth;
        }
    }
    private Camera _camera;
    private CameraEdgeProjection _cameraEdgeProjection;
    private BlackbarController _controller;
    void Start(){
        _camera = Camera.main;
        _cameraEdgeProjection = _camera.GetComponent<CameraEdgeProjection>();
        _controller = transform.parent.gameObject.GetComponent<BlackbarController>();
    }
    void FixedUpdate()
    {
        // calculate anchors
        Vector3 anchorBottomLeft = _cameraEdgeProjection.getProjection(_controller.origin,bottomLeftAngle);
        Vector3 anchorBottomRight = _cameraEdgeProjection.getProjection(_controller.origin,bottomRightAngle);
        anchorBottomLeft = _camera.WorldToViewportPoint(anchorBottomLeft);
        anchorBottomRight = _camera.WorldToViewportPoint(anchorBottomRight);

        Vector3 anchorTopLeft = _cameraEdgeProjection.roundVectorToViewportCorner(anchorBottomLeft,true);
        Vector3 anchorTopRight = _cameraEdgeProjection.roundVectorToViewportCorner(anchorBottomRight,false);


        // calculate rays
        Ray topLeftRay = _camera.ViewportPointToRay(anchorTopLeft);
        Ray topRightRay = _camera.ViewportPointToRay(anchorTopRight);
        Ray bottomLeftRay = _camera.ViewportPointToRay(anchorBottomLeft);
        Ray bottomRightRay = _camera.ViewportPointToRay(anchorBottomRight);

        // calculate world targets
        Vector3 targetTopLeftFront = topLeftRay.GetPoint(distance);
        Vector3 targetTopLeftBack = topLeftRay.GetPoint(distance + 5);
        Vector3 targetTopRightFront = topRightRay.GetPoint(distance);
        Vector3 targetTopRightBack = topRightRay.GetPoint(distance + 5);
        Vector3 targetBottomLeftFront = bottomLeftRay.GetPoint(distance);
        Vector3 targetBottomLeftBack = bottomLeftRay.GetPoint(distance + 5);
        Vector3 targetBottomRightFront = bottomRightRay.GetPoint(distance);
        Vector3 targetBottomRightBack = bottomRightRay.GetPoint(distance + 5);

        // calculate bottom positions

        // bottom should be flat so objects aren't pushed off of platform
        /*targetBottomLeftBack.y = targetBottomLeftFront.y;
        targetBottomRightBack.y = targetBottomRightFront.y;

        // sides should be as wide as the back to ensure bottom is flat
        targetBottomLeftBack.x = targetBottomLeftFront.x;
        targetBottomRightBack.x = targetBottomRightFront.x;*/

        // set all positions
        topLeftFront.position = targetTopLeftFront;
        topLeftBack.position = targetTopLeftBack;
        topRightFront.position = targetTopRightFront;
        topRightBack.position = targetTopRightBack;
        bottomLeftFront.position = targetBottomLeftFront;
        bottomLeftBack.position = targetBottomLeftBack;
        bottomRightFront.position = targetBottomRightFront;
        bottomRightBack.position = targetBottomRightBack;

        Debug.DrawLine(_controller.origin,targetBottomLeftFront);
        Debug.DrawLine(_controller.origin,targetBottomRightFront);
    }
}

