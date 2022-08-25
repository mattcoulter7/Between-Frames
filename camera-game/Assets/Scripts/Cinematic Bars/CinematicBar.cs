using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBar : MonoBehaviour
{
    public bool useFrontInstead = false;
    public float rotationOffset = 0f; // rotation in degrees
    public bool rotateToCamera = false;
    public float rotation
    {
        get
        {
            return _controller.rotationSnapped + rotationOffset;
        }
    }
    public float distanceOffset = 0f; // distance offset from ellipse position
    public float distance
    {
        get
        {
            return _controller.distanceSnapped + distanceOffset;
        }
    }
    public Vector2 origin
    {
        get
        {
            return _controller.offsetSnapped;
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
    private void Start()
    {
        _controller = GetComponentInParent<CinematicBarManager>();
        _rectanglePoints = GetComponent<RectanglePoints>();
        _camera = Camera.main;
        _cameraEdgeProjection = _camera.GetComponent<CameraEdgeProjection>();

        Resize();
    }
    private void Resize()
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


        // update transform values
        // calculate x rotation so that you do not see the depth of the rectangle
        transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x, _camera.transform.eulerAngles.y, rotation);


        ///transform.LookAt(_camera.transform);
        _rectanglePoints.backleft = worldPos; // must set last as position is determined by rotation and scale
                                              //Debug.DrawLine(transform.position,_rectanglePoints.backleft);
    }

    // Update is called once per frame
    private void Update()
    {
        Resize();
    }
}
