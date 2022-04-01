using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarDebug : MonoBehaviour
{
    public bool logBottomBack = false;
    public GameObject top;
    public GameObject bottom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
