using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBar : MonoBehaviour
{
    public bool fixPlayArea = false;
    public bool useFrontInstead = false;
    public float rotationOffset = 0f; // rotation in degrees
    public float rotation
    {
        get
        {
            return _controller.rotation + rotationOffset;
        }
    }
    public float distanceOffset = 0f; // distance offset from ellipse position
    public float distance
    {
        get
        {
            return _controller.distance + distanceOffset;
        }
    }
    public Vector2 origin
    {
        get
        {
            return _controller.offset;
        }
    }
    public float depth = 10f;
    private CinematicBarManager _controller;
    private RectanglePoints _rectanglePoints;
    private Camera _camera;
    private CameraEdgeProjection _cameraEdgeProjection;
    Vector2 GetAspectRatio()
    {
        return _controller.maintainPlayableArea ? new Vector2(
            Screen.height,
            Screen.width
        ).normalized : new Vector2(1, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponentInParent<CinematicBarManager>();
        _rectanglePoints = GetComponent<RectanglePoints>();
        _camera = Camera.main;
        _cameraEdgeProjection = _camera.GetComponent<CameraEdgeProjection>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 aspectRatio = GetAspectRatio();
        // establish target position on top, left, bottom, right of screen
        // aspect ratio is used to scale the ellipse to a circle if desired
        Vector2 top = new Vector2(0.5f, 1f) * aspectRatio;
        Vector2 left = new Vector2(0f, 0.5f) * aspectRatio;
        Vector2 bottom = new Vector2(0.5f, 0) * aspectRatio;
        Vector2 right = new Vector2(1f, 0.5f) * aspectRatio;

        // draw an ellipse through these points
        Vector2 radii = new Vector2(
            right.x - left.x,
            top.y - bottom.y
        ) / 2;

        // calculate target position based on rotation
        Vector3 target = new Vector3(
            origin.x + radii.x * Mathf.Cos(rotation * Mathf.Deg2Rad),
            origin.y + radii.y * Mathf.Sin(rotation * Mathf.Deg2Rad),
            depth
        );

        Vector3 worldPos = _camera.ViewportToWorldPoint(target); // convert target point to world point
        worldPos -= transform.right * distance; // move the points inwards or outwards from the origin



        // calculate the width to ensure no gaps behind
        // get screen intersection based on transform.right
        /*float angle = Vector2.SignedAngle(Vector2.right,transform.right);
        Vector3 screenIntersection = _cameraEdgeProjection.getCornerFromAngle(angle);
        screenIntersection.z = depth;
        screenIntersection = _camera.ViewportToWorldPoint(screenIntersection);
        // get vector from worldpos and screenintersection
        Vector3 toScreenIntersection = screenIntersection - worldPos;*/
        /*transform.localScale = new Vector3(
            toScreenIntersection.magnitude,
            transform.localScale.y,
            transform.localScale.z
        );*/

        // update transform values
        transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x, _camera.transform.eulerAngles.y, rotation);

        _rectanglePoints.backleft = worldPos; // must set last as position is determined by rotation and scale
                                              //Debug.DrawLine(transform.position,_rectanglePoints.backleft);


        // due to perspective, the gap between the two objects becomes inconsitent.
        // we can fix this by casting a ray at the front position, then setting the back position
        // (x,y) values to the ray at a consistent depth
        // if the object position is > 50% of screen, cast at back, set at front

        if (fixPlayArea)
        {
            Vector3 frontViewportPos = _camera.WorldToViewportPoint(_rectanglePoints.frontLeft);
            Vector3 v1 = -transform.up; // down
            Vector3 v2 = new Vector3(frontViewportPos.x - 0.5f, frontViewportPos.y - 0.5f);
            float crossProduct = v1.x * v2.y - v1.y * v2.x;
            if (crossProduct < 0)
            {
                //Debug.Log("Overlap");
                Vector3 backViewportPos = _camera.WorldToViewportPoint(_rectanglePoints.backleft);
                Ray backRay = _camera.ViewportPointToRay(backViewportPos);
                float angle = Vector3.SignedAngle(Vector3.forward, backRay.direction, Vector3.right) * Mathf.Deg2Rad;
                float scaledDistance = (depth - transform.localScale.z) / Mathf.Cos(angle);
                Vector3 backPoint = backRay.GetPoint(scaledDistance);
                _rectanglePoints.frontLeft = worldPos = backPoint;
            }
        }

    }
}
