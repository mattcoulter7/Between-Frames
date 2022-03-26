using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAroundScreen2 : MonoBehaviour
{
    public Vector3 origin = new Vector3(0, 0, 0);
    public float degrees = 0f;
    private readonly Plane[] planes = new Plane[6];
    // Start is called before the first frame update

    float nfmod(float a, float b)
    {
        // custom modulus function to ensure a positive number
        return a - b * Mathf.Floor(a / b);
    }

    // Update is called once per frame
    void Update()
    {
        degrees = nfmod(degrees,360);
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
        transform.position = hitPoint;
        Debug.Log(hitPoint);

        Debug.DrawLine(origin,hitPoint,Color.red);
        // Now the hitPoint should contain the point where your ray hits the screen frustrum/"border"
        //lineRenderer.SetPosition(1, hitPoint);
    }
}

/*
public class SpinAroundScreen2 : MonoBehaviour
{
    public Vector3 origin = new Vector3(0, 0, 0);
    public float degrees = 0f;
    private readonly Plane[] planes = new Plane[6];
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 hitPoint = Camera.main.GetComponent<CameraEdgeProjection>().getProjection(origin,degrees);
        transform.position = hitPoint;

        Debug.DrawLine(origin,hitPoint,Color.red);
    }
}
*/