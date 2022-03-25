using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAroundScreen : MonoBehaviour
{
    public float degrees = 0f;
    Vector3 rotatePoint(Vector3 point)
    {
        degrees = degrees % 360;
        long bigNumber = 99999999999999;
        float radians = degrees * Mathf.PI / 180;
        float m = Mathf.Tan(radians);


        float verticalX = bigNumber / (bigNumber - m);
        if (degrees > 135 && degrees < 225){
            verticalX *= -1;
        }
        
        float horizontalX = 1 / m;
        if (degrees > 225 && degrees < 315){
            horizontalX *= -1;
        }

        if ((degrees > -45 && degrees < 45) || (degrees > 135 && degrees < 225) || (degrees > 315 && degrees < 360))
        {
            point.x = verticalX;
        }
        else
        {
            point.x = horizontalX;
        }
        point.y = m * point.x;
        return point;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 viewportPoint = new Vector3(0, 0, 0);
        Vector3 rotatedPoint = rotatePoint(viewportPoint);

        // translate point to viewport coords
        rotatedPoint.x = rotatedPoint.x / 2 + 0.5f;
        rotatedPoint.y = rotatedPoint.y / 2 + 0.5f;

        Ray ray = Camera.main.ViewportPointToRay(rotatedPoint);
        Vector3 worldPoint = ray.GetPoint(10f);

        Debug.Log(rotatedPoint);
        Debug.Log(worldPoint);
        transform.position = worldPoint;
    }
}
