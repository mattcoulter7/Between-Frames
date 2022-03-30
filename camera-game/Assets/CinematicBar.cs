using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBar : MonoBehaviour
{
    public Transform lookAt;
    public float rotationOffset = 0f; // rotation in degrees
    public float rotation {
        get {
            return _controller.rotation + rotationOffset;
        }
    }
    public float distanceOffset = 0f; // distance offset from ellipse position
    public float distance {
        get {
            return _controller.distance + distanceOffset;
        }
    }
    public Vector2 origin {
        get {
            return _controller.offset;
        }
    }
    private CinematicBars _controller;
    private RectanglePoints _rectanglePoints;
    private Camera _camera;
    private CameraEdgeProjection _cameraEdgeProjection;
    private Vector2 aspectRatio {
        get {
            return _controller.maintainPlayableArea ? new Vector2(
                Screen.height,
                Screen.width
            ).normalized : new Vector2(1,1); 
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponentInParent<CinematicBars>();
        _rectanglePoints = GetComponent<RectanglePoints>();
        _camera = Camera.main;
        _cameraEdgeProjection = _camera.GetComponent<CameraEdgeProjection>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // establish target position on top, left, bottom, right of screen
        Vector2 top = new Vector2(0.5f,1f) * aspectRatio + origin;
        Vector2 left = new Vector2(0f,0.5f) * aspectRatio + origin; 
        Vector2 bottom = new Vector2(0.5f,0) * aspectRatio + origin;
        Vector2 right = new Vector2(1f,0.5f) * aspectRatio + origin;

        // draw an ellipse through these points
        Vector2 radii = new Vector2(
            right.x - left.x,
            top.y - bottom.y
        ) / 2;
        
        // calculate target position based on rotation
        Vector3 target = new Vector3(
            origin.x + radii.x * Mathf.Cos(rotation * Mathf.Deg2Rad),
            origin.y + radii.y * Mathf.Sin(rotation * Mathf.Deg2Rad),
            10
        );

        Vector3 worldPos = _camera.ViewportToWorldPoint(target) - (transform.right * distance);
        //transform.LookAt(lookAt); // maybeeeee

        // calculate the width to ensure no gaps behind
        // get screen intersection based on transform.right
        float angle = Vector2.SignedAngle(Vector2.right,transform.right);
        Vector3 screenIntersection = _cameraEdgeProjection.getCornerFromAngle(angle);
        screenIntersection.z = 10;
        screenIntersection = _camera.ViewportToWorldPoint(screenIntersection);
        
        // get vector from worldpos and screenintersection
        Vector3 toScreenIntersection = screenIntersection - worldPos;
        
        // set transform.localScale.y to magnitude of this vector
        _rectanglePoints.left = worldPos; //Vector3.Lerp(_rectanglePoints.left,worldPos,0.5f);
        transform.rotation = Quaternion.Euler(0,0,rotation);
        /*transform.localScale = new Vector3(
            toScreenIntersection.magnitude,
            transform.localScale.y,
            transform.localScale.z
        );*/
    }
}
