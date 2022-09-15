using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class automatically adjust the box collider to fit the surface of 4 points.
/// https://gamedev.stackexchange.com/questions/202242/calculate-normal-and-plane-orientation-using-4-3d-points
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class BoxColliderSurfaceMounter2 : MonoBehaviour
{
    /// <summary>
    /// Top left
    /// </summary>
    public Transform A;

    /// <summary>
    /// Top Right
    /// </summary>
    public Transform B;

    /// <summary>
    /// Bottom Left
    /// </summary>
    public Transform C;

    /// <summary>
    /// Bottom Right
    /// </summary>
    public Transform D;
    public Vector2 positionAdjustment;
    public float rotationAdjustment;

    public float height;

    private Vector3 x;
    private Vector3 y;
    private Vector3 z;
    private Quaternion planeOrientation;

    // Update is called once per frame
    void Update()
    {
        // Calculate the correct rotation
        x = (C.position - A.position + D.position - B.position).normalized;
        y = (C.position - D.position + A.position - B.position).normalized;
        z = Vector3.Cross(x, y).normalized;
        planeOrientation = Quaternion.LookRotation(z, y);

        transform.eulerAngles = planeOrientation.eulerAngles + new Vector3(0,0, rotationAdjustment);

        // Calculate the Target Position
        Vector3 targetPosition = (A.position + B.position + C.position + D.position) / 4;
        targetPosition += z.normalized * height / 2;
        targetPosition += transform.right * positionAdjustment.y + transform.up * positionAdjustment.x;
        transform.position = targetPosition;

        // Calculate the Target Scale
        Vector3 AB = B.position - A.position;
        Vector3 AC = C.position - A.position;

        Vector3 targetScale = new Vector3(AB.magnitude, AC.magnitude, height);
        transform.localScale = targetScale;
    }
}
