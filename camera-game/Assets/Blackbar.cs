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
    public float rotation = 90f;
    public float leftAngleDepth = 20f; // the left height in viewport units
    public float rightAngleDepth = 20f; // the right height in viewport units
    float topLeftAngle
    {
        get
        {
            return rotation + 45;
        }
    }
    float topRightAngle
    {
        get
        {
            return rotation - 45;
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
    float nfmod(float a, float b)
    {
        // custom modulus function to ensure a positive number
        return a - b * Mathf.Floor(a / b);
    }

    Vector3 getViewportPointFromAngle(float degrees)
    {
        Vector3 point = new Vector3(0, 0, 0);
        degrees = nfmod(degrees,360);
        long bigNumber = 99999999999999;
        float radians = degrees * Mathf.PI / 180;
        float m = Mathf.Tan(radians);

        float verticalX = bigNumber / (bigNumber - m);
        if (degrees >= 135 && degrees <= 225)
        {
            verticalX *= -1;
        }

        float horizontalX = 1 / m;
        if (degrees >= 225 && degrees <= 315)
        {
            horizontalX *= -1;
        }

        if ((degrees >= -45 && degrees <= 45) || (degrees >= 135 && degrees <= 225) || (degrees >= 315 && degrees <= 360))
        {
            point.x = verticalX;
        }
        else
        {
            point.x = horizontalX;
        }
        point.y = m * point.x;
        // transform to be in bounds of (0,1)
        point.x = point.x / 2 + 0.5f;
        point.y = point.y / 2 + 0.5f;
        return point;
    }

    Vector3 roundVectorToViewportCorner(float basedOnAngle, bool flip)
    {
        basedOnAngle = nfmod(basedOnAngle,360);
        if (basedOnAngle <= 45 || basedOnAngle > 315){ // right
            return flip ? new Vector3(1,0,0) : new Vector3(1,1,0); // bottom right | top right
        } else if (basedOnAngle <= 135 && basedOnAngle > 45){ // top
            return flip ? new Vector3(1,1,0) : new Vector3(0,1,0);
        } else if (basedOnAngle <= 225 && basedOnAngle > 135){ // left
            return flip ? new Vector3(0,1,0) : new Vector3(0,0,0);
        } else if (basedOnAngle <= 315 && basedOnAngle > 225){ // bottom
            return flip ? new Vector3(0,0,0) : new Vector3(1,0,0);
        }
        return new Vector3();
    }
    void FixedUpdate()
    {
        // calculate anchors
        Vector3 anchorBottomLeft = getViewportPointFromAngle(bottomLeftAngle);
        Vector3 anchorBottomRight = getViewportPointFromAngle(bottomRightAngle);
        Vector3 anchorTopLeft = roundVectorToViewportCorner(bottomLeftAngle,true);
        Vector3 anchorTopRight = roundVectorToViewportCorner(bottomRightAngle,false);

        // calculate rays
        Ray topLeftRay = Camera.main.ViewportPointToRay(anchorTopLeft);
        Ray topRightRay = Camera.main.ViewportPointToRay(anchorTopRight);
        Ray bottomLeftRay = Camera.main.ViewportPointToRay(anchorBottomLeft);
        Ray bottomRightRay = Camera.main.ViewportPointToRay(anchorBottomRight);

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

        /*// bottom should be flat so objects aren't pushed off of platform
        targetBottomLeftBack.y = targetBottomLeftFront.y;
        targetBottomRightBack.y = targetBottomRightFront.y;

        // sides should be as wide as the back to ensure bottom is flat
        targetBottomLeftFront.x = targetBottomLeftBack.x;
        targetBottomRightFront.x = targetBottomRightBack.x;
        targetTopLeftFront.x = targetTopLeftBack.x;
        targetTopRightFront.x = targetTopRightBack.x;

        // recalculate origin
        Ray middleRay = Camera.main.ViewportPointToRay(new Vector3(0.5f,0,0));
        Vector3 newOrigin = middleRay.GetPoint(distance);
        newOrigin.y = (targetTopLeftFront - targetBottomLeftBack).y;
        
        // set new origin (ensures object is still rendered)
        transform.position = newOrigin;*/

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

