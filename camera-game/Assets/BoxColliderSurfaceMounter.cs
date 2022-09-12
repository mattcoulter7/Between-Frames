using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class automatically adjust the box collider to fit the surface of 4 points.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class BoxColliderSurfaceMounter : MonoBehaviour
{
    public Transform bottomLeft;
    public Transform bottomRight;
    public Transform topLeft;
    public Transform topRight;

    public float height;

    private BoxCollider _boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Quaternion CalculateAngle(Transform p1, Transform p2)
    {
        Vector3 dir = p2.position - p1.position; // a vector pointing from pointA to pointB
        Quaternion rot = Quaternion.LookRotation(dir, Vector3.up); // calc a rotation that
        return rot;
    }

    // Update is called once per frame
    void Update()
    {
        // POSITION
        // box position is always in the centre of the four points
        Vector3 center = (bottomLeft.position + bottomRight.position + topLeft.position + topRight.position) / 4;

        // adjust the height of the collider based on half the height (because position is in the middle)
        Vector3 frontToBack = (bottomLeft.position - topLeft.position).normalized;
        Vector3 topToBottom = Vector3.Cross(frontToBack, Vector3.left).normalized;
        center += topToBottom * height / 2;

        // ROTATION
        // Calculate X Rotation (Front to Back)
        float xRotation = CalculateAngle(topLeft, bottomLeft).eulerAngles.x;
        float xRotation2 = CalculateAngle(topRight, bottomRight).eulerAngles.x;

        // Calculate Z Rotation (Left to Right)
        float zRotation = CalculateAngle(topLeft, topRight).eulerAngles.x;

        // SCALE
        // calculate X Scale (width)
        float xScale = (topRight.position - topLeft.position).magnitude;

        Quaternion rotation = CalculateAngle(topLeft, bottomRight);

        // calculate Y Scale (height)
        float yScale = height;

        // calculate Z Scale (depth)
        float zScale = (bottomLeft.position - topLeft.position).magnitude;

        // Apply Values
        transform.position = center;
        transform.eulerAngles = new Vector3(xRotation, 0, zRotation);
        transform.localScale = new Vector3(xScale, yScale, zScale);

    }
}
