using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeProjection : MonoBehaviour
{
    private readonly Plane[] planes = new Plane[6];
    public Vector3 getProjection(Vector3 origin, float degrees){
        degrees = MathUtils.nfmod(degrees,360);
        
        // Wherever you get these to from
        Vector3 dir = Quaternion.Euler(0,0,degrees) * Vector3.right;
        var ray = new Ray(origin, dir);

        var currentMinDistance = float.MaxValue;
        var hitPoint = Vector3.zero;
        GeometryUtility.CalculateFrustumPlanes(Camera.main, planes);
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

    public Vector3 roundVectorToViewportCorner(float basedOnAngle, bool flip)
    {
        basedOnAngle = MathUtils.nfmod(basedOnAngle,360);
        if (basedOnAngle <= 45 || basedOnAngle > 315){ // right
            return flip ? new Vector3(1,0,0) : new Vector3(1,1,0); // bottom right | top right
        } else if (basedOnAngle <= 135 && basedOnAngle > 45){ // top
            return flip ? new Vector3(1,1,0) : new Vector3(0,1,0);
        } else if (basedOnAngle <= 225 && basedOnAngle > 135){ // left
            return flip ? new Vector3(0,1,0) : new Vector3(0,0,0);
        } else if (basedOnAngle <= 315 && basedOnAngle > 225){ // bottom
            return flip ? new Vector3(0,0,0) : new Vector3(1,0,0);
        }
        return new Vector3();
    }
}
