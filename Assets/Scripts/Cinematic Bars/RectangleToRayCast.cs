using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This enabled 3D bounds of a 2D face to be calculated. 
/// This works by casting rays from Camera.main to each of the 4 edges of a rectangle.
/// Then, using the Depth and Distance variables, the front and back points can all be calculated along the rays
/// </summary>
public class RectangleToRayCast : BoneWeightedBoxController
{
    /// <summary>The distance refers to how far on the ray is the first front face</summary>
    public float distance = 10f;

    /// <summary>The depth referes to how far from the front face along the rays is the rear face</summary>
    public float depth = 5f;

    /// <summary>rectanglePoints is a reference to the RectanglePoints component which determine the current point vectors of a Rectangular Prism</summary>
    public RectanglePoints rectanglePoints;

    private float getScaledDistance(Ray ray)
    {
        float angle = Vector3.SignedAngle(Vector3.forward, ray.direction, Vector3.right) * Mathf.Deg2Rad;
        float scaledDistance = distance / Mathf.Cos(angle);
        return scaledDistance;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 targetTopLeft = rectanglePoints.frontTopLeft;
        Vector3 targetBottomLeft = rectanglePoints.frontBottomLeft;
        Vector3 targetTopRight = rectanglePoints.frontTopRight;
        Vector3 targetBottomRight = rectanglePoints.frontBottomRight;

        Ray topLeftRay = Camera.main.ViewportPointToRay(
            Camera.main.WorldToViewportPoint(targetTopLeft)
        );
        Ray topRightRay = Camera.main.ViewportPointToRay(
            Camera.main.WorldToViewportPoint(targetTopRight)
        );
        Ray bottomLeftRay = Camera.main.ViewportPointToRay(
            Camera.main.WorldToViewportPoint(targetBottomLeft)
        );
        Ray bottomRightRay = Camera.main.ViewportPointToRay(
            Camera.main.WorldToViewportPoint(targetBottomRight)
        );

        float topLeftDist = getScaledDistance(topLeftRay);
        float topRight = getScaledDistance(topRightRay);
        float bottomLeft = getScaledDistance(bottomLeftRay);
        float bottomRight = getScaledDistance(bottomRightRay);

        topLeftFront.position = topLeftRay.GetPoint(topLeftDist);
        topLeftBack.position = topLeftRay.GetPoint(topLeftDist + depth);
        topRightFront.position = topRightRay.GetPoint(topRight);
        topRightBack.position = topRightRay.GetPoint(topRight + depth);
        bottomLeftFront.position = bottomLeftRay.GetPoint(bottomLeft);
        bottomLeftBack.position = bottomLeftRay.GetPoint(bottomLeft + depth);
        bottomRightFront.position = bottomRightRay.GetPoint(bottomRight);
        bottomRightBack.position = bottomRightRay.GetPoint(bottomRight + depth);

        Debug.DrawLine(Camera.main.transform.position, targetTopLeft);
        Debug.DrawLine(Camera.main.transform.position, targetBottomLeft);
        Debug.DrawLine(Camera.main.transform.position, targetTopRight);
        Debug.DrawLine(Camera.main.transform.position, targetBottomRight);
    }
}
