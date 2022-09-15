using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarReticle : MonoBehaviour
{
    CinematicBarManager _cinematicBarManager;
    CinematicBarDebug _cinematicBarDebug;
    public float depth = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _cinematicBarManager = GetComponentInParent<CinematicBarManager>();
        _cinematicBarDebug = GetComponentInParent<CinematicBarDebug>();
    }

    // Update is called once per frame
    void Update()
    {        
        RectanglePoints top = _cinematicBarDebug.top.GetComponent<RectanglePoints>();
        RectanglePoints bottom = _cinematicBarDebug.bottom.GetComponent<RectanglePoints>();

        Vector3 topScreenPoint = Camera.main.WorldToViewportPoint(top.left);
        Vector3 bottomScreenPoint = Camera.main.WorldToViewportPoint(bottom.left);
        Vector3 viewportMidPoint = (bottomScreenPoint + topScreenPoint) / 2;
        viewportMidPoint.z = depth;
        Vector3 worldMidPoint = Camera.main.ViewportToWorldPoint(viewportMidPoint);

        transform.position = worldMidPoint;
    }
}
