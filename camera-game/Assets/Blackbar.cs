using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackbar : MonoBehaviour
{
    public float cameraYAnchor = 0f; // the height of camera to anchored to (0,1)
    public float localYAnchor = 0f; // the local height to be anchored to the camera (-1,1)
    public float distanceFromCamera = 0f;
    // Start is called before the first frame update
    public Vector3 currentFrontLeft
    {
        get
        {
            Vector3 localPos = new Vector3(-transform.localScale.x / 2, localYAnchor, -transform.localScale.z / 2);
            return transform.position + localPos;
        }
    }
    public Vector3 currentBackLeft
    {
        get
        {
            Vector3 localPos = new Vector3(-transform.localScale.x / 2, localYAnchor, transform.localScale.z / 2);
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
            Vector3 localPos = new Vector3(transform.localScale.x / 2, localYAnchor, -transform.localScale.z / 2);
            return transform.position + localPos;
        }
    }
    public Vector3 currentBackRight
    {
        get
        {
            Vector3 localPos = new Vector3(transform.localScale.x / 2, localYAnchor, transform.localScale.z / 2);
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

    public Vector3 positionOffset
    { // a multiplier to ensure there are no gaps
        get
        {
            float objectWidth = currentFrontRight.x - currentFrontLeft.x;
            float offsetWidth = objectWidth * offsetScale;
            return new Vector3(-offsetWidth,0,0);
        }
    }
    public float offsetScale
    {
        get
        {
            // calculate the object width in pixels
            Vector2 frontLeft = Camera.main.WorldToScreenPoint(currentFrontLeft);
            Vector2 frontRight = Camera.main.WorldToScreenPoint(currentFrontRight);
            float objectWidthPixels = frontRight.x - frontLeft.x;

            // calculate the offset pixels
            Vector2 backLeft = Camera.main.WorldToScreenPoint(currentBackLeft);
            float pixelOffsetDistance = backLeft.x - frontLeft.x;

            // calculate scale
            return pixelOffsetDistance / objectWidthPixels;
        }
    }

    void Start()
    {
        localYAnchor *= transform.localScale.y / 2; // scale according to local height

        Vector3 cameraPos = Camera.main.transform.position;
        distanceFromCamera = (currentFrontRight - cameraPos).z;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Camera.main.transform.position, targetLeft, Color.green);
        Debug.DrawLine(Camera.main.transform.position, targetRight, Color.green);
        Debug.DrawLine(Camera.main.transform.position, currentFrontLeft, Color.red);
        Debug.DrawLine(Camera.main.transform.position, currentFrontRight, Color.red);

        // reposition  to left anchor
        Vector3 toTargetPos = targetLeft - currentFrontLeft;
        transform.position = transform.position + toTargetPos + positionOffset;

        // rescale x to fit screen width
        float currentWidth = (this.currentFrontRight - this.currentFrontLeft).x;
        float targetWidth = (this.targetRight - this.targetLeft).x;

        float currentScale = transform.localScale.x;
        float targetScale = currentScale * (targetWidth / currentWidth) * (1 + offsetScale * 2);

        transform.localScale = new Vector3(targetScale, transform.localScale.y, transform.localScale.z);
    }
}

