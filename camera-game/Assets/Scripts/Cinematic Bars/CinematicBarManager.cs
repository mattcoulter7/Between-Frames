using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarManager : MonoBehaviour
{
    public bool maintainPlayableArea = false;
    public float rotation = 0f;
    public bool offsetSnapping = false;
    public Vector2 offsetSnap = new Vector2(0, 0);
    public Vector2 _offset = new Vector2(0.5f, 0.5f);

    public Vector2 snappedOffset
    {
        get
        {
            Vector2 copy = new Vector2(_offset.x, _offset.y);
            if (offsetSnapping)
            {
                copy.x = MathUtils.roundFloatToStep(copy.x, offsetSnap.x);
                copy.y = MathUtils.roundFloatToStep(copy.y, offsetSnap.y);
            }
            return copy;
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
    public float distance = 0f;
}
