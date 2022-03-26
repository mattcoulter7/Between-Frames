using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAroundScreen2 : MonoBehaviour
{
    public Vector3 origin = new Vector3(0, 0, 0);
    public Vector3 direction = new Vector3(1, 0, 0);
    public float degrees = 0f;
    private readonly Plane[] planes = new Plane[6];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Wherever you get these to from
        Vector2 dir = (Vector2)(Quaternion.Euler(0,0,degrees) * Vector2.right);
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

        Debug.DrawLine(Camera.main.transform.position,hitPoint,Color.red);
        // Now the hitPoint should contain the point where your ray hits the screen frustrum/"border"
        //lineRenderer.SetPosition(1, hitPoint);
    }
}
