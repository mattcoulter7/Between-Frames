using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is responsible calculating the edge points and corner points 
/// of the screen based on angles and origins.
///
/// This script is no longer used.
/// </summary>
public class CameraEdgeProjection : MonoBehaviour
{
    private readonly Plane[] planes = new Plane[6];
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    
    /// <summary>
    /// Calculates the screen intersection based an origin and an angle
    /// </summary>
    /// <returns>World position in line with the edge of the screen</returns>
    /// <param name="origin">The world position where the angle is calculated from</param>
    /// <param name="degrees">The angle in degrees where the projection happens. 0 degrees is twoards the right, like a unit circle</param>
    public Vector3 getProjection(Vector3 origin, float degrees)
    {
        degrees = MathUtils.nfmod(degrees, 360);
        // Wherever you get these to from
        Vector3 dir = Quaternion.Euler(0, 0, degrees) * Vector3.right;
        var ray = new Ray(origin, dir);

        var currentMinDistance = float.MaxValue;
        var hitPoint = Vector3.zero;
        GeometryUtility.CalculateFrustumPlanes(_camera, planes);
        for (var i = 0; i < 4; i++)
        {
            // Raycast against the plane
            if (planes[i].Raycast(ray, out var distance))
            {
                // Since a plane is mathematical infinite
                // what you would want is the one that hits with the shortest ray distance
                if (distance < currentMinDistance)
                {
                    hitPoint = ray.GetPoint(distance);
                    currentMinDistance = distance;
                }
            }
        }
        return hitPoint;
    }

    /// <summary>
    /// Rounds an angle to a Viewport Vector3 corner position
    /// </summary>
    /// <returns>Vector3 representing viewport corner</returns>
    /// <param name="basedOnAngle">the angle in degrees from the center of the screen</param>
    /// <param name="flip">True will grab the corner before the angle. False will grab the corner after the angle (anti-clockwise)</param>
    public Vector3 roundVectorToViewportCorner(float basedOnAngle, bool flip)
    {
        basedOnAngle = MathUtils.nfmod(basedOnAngle, 360);
        if (basedOnAngle <= 45 || basedOnAngle > 315)
        { // right
            return flip ? new Vector3(1, 0, 0) : new Vector3(1, 1, 0); // bottom right | top right
        }
        else if (basedOnAngle <= 135 && basedOnAngle > 45)
        { // top
            return flip ? new Vector3(1, 1, 0) : new Vector3(0, 1, 0); // top right | top left
        }
        else if (basedOnAngle <= 225 && basedOnAngle > 135)
        { // left
            return flip ? new Vector3(0, 1, 0) : new Vector3(0, 0, 0); // top left | top right
        }
        else if (basedOnAngle <= 315 && basedOnAngle > 225)
        { // bottom
            return flip ? new Vector3(0, 0, 0) : new Vector3(1, 0, 0); // bottom left | bottom right
        }
        return new Vector3();
    }

    /// <summary>
    /// Gets the viewport corner position based on an edge viewport position
    /// </summary>
    /// <returns>Vector3 representing viewport corner</returns>
    /// <param name="relativePosition">the Viewport position that lies on an edge</param>
    /// <param name="flip">True will grab the corner before the point. False will grab the corner after the point (anti-clockwise)</param>
    public Vector3 roundVectorToViewportCorner(Vector3 relativePosition, bool flip)
    {
        if (relativePosition.x > 0.9999f && relativePosition.y > 0 && relativePosition.y <= 1)
        { // right
            return flip ? new Vector3(1, 0, 0) : new Vector3(1, 1, 0); // bottom right | top right
        }
        else if (relativePosition.y > 0.9999f && relativePosition.x > 0 && relativePosition.x <= 1)
        { // top
            return flip ? new Vector3(1, 1, 0) : new Vector3(0, 1, 0);
        }
        else if (relativePosition.x < 0.0001f && relativePosition.y >= 0 && relativePosition.y < 1)
        { // left
            return flip ? new Vector3(0, 1, 0) : new Vector3(0, 0, 0);
        }
        else if (relativePosition.y < 0.0001f && relativePosition.x >= 0 && relativePosition.x < 1)
        { // bottom
            return flip ? new Vector3(0, 0, 0) : new Vector3(1, 0, 0);
        }
        return new Vector3();
    }

    /// <summary>
    /// Get a Vector3 viewpoint position based off of an angle of the origin of the screen
    /// </summary>
    /// <returns>Vector3 representing viewport corner</returns>
    /// <param name="degrees">The angle where 0 is right</param>
    public Vector3 getCornerFromAngle(float degrees){
        degrees = MathUtils.nfmod(degrees,360);
        if (degrees >= 0 && degrees < 90) {
            return new Vector3(1,1,0); // top right
        } else if (degrees >= 90 && degrees < 180) {
            return new Vector3(0,1,0); // top left
        } else if (degrees >= 180 && degrees < 270) {
            return new Vector3(0,0,0); // bottom left
        } else if (degrees >= 270 && degrees < 360) {
            return new Vector3(1,0,0); // bottom right
        }
        return new Vector3();
    }
}
