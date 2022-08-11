using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for 
/// </summary>
public class CinematicBar : MonoBehaviour
{
    /// <summary>Rotation in degrees offset from the CinematicBarManager rotation</summary>
    public float rotationOffset = 0f;

    /// <summary>Rotation in degrees after rotationOffset is combined with the CinematicBarManager rotation</summary>
    public float rotation
    {
        get
        {
            return _controller.rotationSnapped + rotationOffset;
        }
    }
    
    /// <summary>Distance offset from CinematicBarManager distance</summary>
    public float distanceOffset = 0f;
    
    /// <summary>Distance after distanceOffset is combined with the CinematicBarManager distance</summary>
    public float distance
    {
        get
        {
            return _controller.distanceSnapped + distanceOffset;
        }
    }
    
    /// <summary>The CinematicBarManager's offset</summary>
    public Vector2 origin
    {
        get
        {
            return _controller.offsetSnapped;
        }
    }
    
    /// <summary>The distance from the camera</summary>
    public float depth = 10f;

    private CinematicBarManager _controller;
    private RectanglePoints _rectanglePoints;
    private Camera _camera;
    private CameraEdgeProjection _cameraEdgeProjection;

    private Vector2 GetAspectRatio()
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
    }

    // Update is called once per frame
    private void Update()
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
}
