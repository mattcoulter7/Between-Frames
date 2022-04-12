using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleToRayCast : BoneWeightedBoxController
{
    public GameObject cinematicBar;
    public float distance = 10f;
    public float depth = 5f;
    private RectanglePoints _rectanglePoints;
    // Start is called before the first frame update
    void Start()
    {
        _rectanglePoints = cinematicBar.GetComponent<RectanglePoints>();
    }

    private float getScaledDistance(Ray ray)
    {
        float angle = Vector3.SignedAngle(Vector3.forward, ray.direction, Vector3.right) * Mathf.Deg2Rad;
        float scaledDistance = distance / Mathf.Cos(angle);
        return scaledDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetTopLeft = _rectanglePoints.frontTopLeft;
        Vector3 targetBottomLeft = _rectanglePoints.frontBottomLeft;
        Vector3 targetTopRight = _rectanglePoints.frontTopRight;
        Vector3 targetBottomRight = _rectanglePoints.frontBottomRight;

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
