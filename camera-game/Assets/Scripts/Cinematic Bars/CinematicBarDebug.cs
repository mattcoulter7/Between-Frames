using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for showing debug information related to the
/// Cinematic Bars for debug purposes only
/// </summary>
public class CinematicBarDebug : MonoBehaviour
{
    /// <summary>Draws lines from camera to the back bottom 2 points of the bars</summary>
    public bool logBottomBack = false;
    
    /// <summary>Reference to the Top Cinematic Bar Target gameObject
    public GameObject top;

    /// <summary>Reference to the Bottom Cinematic Bar Target gameObject
    public GameObject bottom;

    /// <summary>
    /// Calculates the point inbetween two inner middle points of both cinematic bars
    /// </summary>
    /// <returns>The Vector3 of middle point between inner points of the cinematic bars</returns>
    public Vector3 GetMiddlePoint(){
        RectanglePoints topRectanglePoints = top.GetComponent<RectanglePoints>();
        RectanglePoints bottomRectanglePoints = bottom.GetComponent<RectanglePoints>();

        return topRectanglePoints.left + (bottomRectanglePoints.left - topRectanglePoints.left) / 2;
    }

    // Update is called once per frame
    private void Update()
    {
        RectanglePoints topRectanglePoints = top.GetComponent<RectanglePoints>();
        RectanglePoints bottomRectanglePoints = bottom.GetComponent<RectanglePoints>();
        if (logBottomBack){
            Debug.DrawLine(topRectanglePoints.backleft,bottomRectanglePoints.backleft);
            Vector3 connectingLine = topRectanglePoints.backleft - bottomRectanglePoints.backleft;
            Debug.Log(connectingLine.magnitude);

            Vector3 topScreenPoint = Camera.main.WorldToViewportPoint(topRectanglePoints.backleft);
            Vector3 bottomScreenPoint = Camera.main.WorldToViewportPoint(bottomRectanglePoints.backleft);
            Vector3 screenConnectingLine = topScreenPoint - bottomScreenPoint;
            Debug.Log(screenConnectingLine.magnitude);
        }   
    }
}
