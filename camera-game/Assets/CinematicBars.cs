using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBars : MonoBehaviour
{
    public bool maintainPlayableArea = false;
    public float rotation = 0f;
    public Vector2 _offset = new Vector2(0.5f,0.5f);
    public Vector2 offset {
        get {
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
