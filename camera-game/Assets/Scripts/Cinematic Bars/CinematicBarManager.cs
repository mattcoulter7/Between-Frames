using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarManager : MonoBehaviour
{
    public bool maintainPlayableArea = false;
    public float rotation = 0f;
    public float rotationSnapped { 
        get
        {
            return rotationSnapping ? MathUtils.roundFloatToStep(rotation, rotationSnap) : rotation;
        }
    }
    public bool rotationSnapping = false;
    public float rotationSnap = 5f; // 5 degrees
    public bool offsetSnapping = false;
    public Vector2 offsetSnap = new Vector2(0.05f, 0.05f);
    public Vector2 _offset = new Vector2(0.5f, 0.5f);

    public Vector2 snappedOffset
    {
        get
        {
            return offsetSnapping ? new Vector2(
                MathUtils.roundFloatToStep(_offset.x, offsetSnap.x),
                MathUtils.roundFloatToStep(_offset.y, offsetSnap.y)
            ) : _offset;
        }
    }
    public Vector2 offset {
        get
        {
            return _offset;
        }
        set {
            // ensure origin is always in the screen
            value.x = Mathf.Max(value.x,0);
            value.x = Mathf.Min(value.x,1);
            value.y = Mathf.Max(value.y,0);
            value.y = Mathf.Min(value.y,1);
            _offset = value;
        }
    }
    public bool distanceSnapping = false;
    public float distance = 0f;
    public float distanceSnapped
    {
        get
        {
            return distanceSnapping ? MathUtils.roundFloatToStep(distance, distanceSnap) : distance;
        }
    }
    public float distanceSnap = 5f; // 5 degrees
}
