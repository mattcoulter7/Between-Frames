using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackbar : MonoBehaviour
{
    public float cameraYAnchor = 0f; // the height of camera to anchored to (0,1)
    public float localYAnchor = 0f; // the local height to be anchored to the camera (-1,1)
    public float scaledYAnchor {
        get {
            return localYAnchor * transform.localScale.y / 2; // scale according to local height
        }
    }
    public float distanceFromCamera {
        get {
            Vector3 cameraPos = Camera.main.transform.position;
            return originalDepth - cameraPos.z;
        }
    }
    private float originalDepth = 0f;
    // Start is called before the first frame update
    public Vector3 currentFrontLeft
    {
        get
        {
            Vector3 localPos = new Vector3(-transform.localScale.x / 2, scaledYAnchor, -transform.localScale.z / 2);
            return transform.position + localPos;
        }
    }
    public Vector3 currentBackLeft
    {
        get
        {
            Vector3 localPos = new Vector3(-transform.localScale.x / 2, scaledYAnchor, transform.localScale.z / 2);
            return transform.position + localPos;
        }
    }
    public Vector3 targetLeft
    {
        get
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0, cameraYAnchor, 0));
            float originOffset = Camera.main.transform.position.z - ray.origin.z; // since ray cast origin is in front of camera, need to account for that
            float distance = (distanceFromCamera + originOffset) / ray.direction.z;
            return ray.GetPoint(distance);
        }
    }
    public Vector3 currentFrontRight
    {
        get
        {
            Vector3 localPos = new Vector3(transform.localScale.x / 2, scaledYAnchor, -transform.localScale.z / 2);
            return transform.position + localPos;
        }
    }
    public Vector3 currentBackRight
    {
        get
        {
            Vector3 localPos = new Vector3(transform.localScale.x / 2, scaledYAnchor, transform.localScale.z / 2);
            return transform.position + localPos;
        }
    }
    public Vector3 targetRight
    {
        get
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(1, cameraYAnchor, 0));
            float originOffset = Camera.main.transform.position.z - ray.origin.z; // since ray cast origin is in front of camera, need to account for that
            float distance = (distanceFromCamera + originOffset) / ray.direction.z;
            return ray.GetPoint(distance);
        }
    }

    public float positionOffset
    { 
        // a value in world units of shift to ensure there are no gaps in black bars
        get
        {
            float objectWidth = currentFrontRight.x - currentFrontLeft.x;
            return objectWidth * offsetScale;
        }
    }
    public float pixelsWidth {
        get {
            // calculate the object width in pixels
            Vector2 frontLeft = Camera.main.WorldToScreenPoint(currentFrontLeft);
            Vector2 frontRight = Camera.main.WorldToScreenPoint(currentFrontRight);
            return frontRight.x - frontLeft.x;
        }
    }
    public float pixelsOffset {
        get {
            // calculate the offset pixels
            Vector2 frontLeft = Camera.main.WorldToScreenPoint(currentFrontLeft);
            Vector2 backLeft = Camera.main.WorldToScreenPoint(currentBackLeft);
            return backLeft.x - frontLeft.x;
        }
    }
    public float offsetScale
    {
        get
        {
            return pixelsOffset / pixelsWidth;
        }
    }

    void Start()
    {
        originalDepth = currentFrontRight.z;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Camera.main.transform.position, targetLeft, Color.green);
        Debug.DrawLine(Camera.main.transform.position, targetRight, Color.green);
        Debug.DrawLine(Camera.main.transform.position, currentFrontLeft, Color.red);
        Debug.DrawLine(Camera.main.transform.position, currentFrontRight, Color.red);


        // rescale x to fit screen width
        float currentWidth = transform.localScale.x;
        float targetWidth = (this.targetRight - this.targetLeft).x + (positionOffset * 2);

        // reposition  to left anchor
        Vector3 toTargetPos = targetLeft - currentFrontLeft;

        transform.localScale = new Vector3(targetWidth, transform.localScale.y, transform.localScale.z);
        transform.position = transform.position + toTargetPos - new Vector3(positionOffset,0,0);
    }
}

