using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CinematicBarManager : MonoBehaviour
{
    public bool maintainPlayableArea = false;

    [Header("Rotation")]
    public float _rotation = 0f;
    public float rotation
    {
        get
        {
            return _rotation;
        }
        set
        {
            if (enableRotationLimit)
            {
                value = Mathf.Clamp(value, -maxRotation, maxRotation);
            }
            _rotation = value;
        }
    }
    public float rotationSnapped
    {
        get
        {
            return enableRotationSnapping ? MathUtils.roundFloatToStep(rotation, rotationSnap) : rotation;
        }
    }
    public bool enableRotationSnapping = false;
    public float rotationSnap = 5f;
    public bool enableRotationLimit = false;
    public float maxRotation = 0f;

    [Header("Offset")]
    public Vector2 _offset = new Vector2(0.5f, 0.5f);
    public Vector2 offset
    {
        get
        {
            return _offset;
        }
        set
        {
            // ensure origin is always in the screen
            if (enableOffsetLimit)
            {
                
                value.x = Mathf.Clamp(value.x, minOffset.x, maxOffset.x);
                value.y = Mathf.Clamp(value.y, minOffset.y, maxOffset.y);
            }
            _offset = value;
        }
    }
    public Vector2 offsetSnapped
    {
        get
        {
            return enableOffsetSnapping ? new Vector2(
                MathUtils.roundFloatToStep(offset.x, offsetSnap.x),
                MathUtils.roundFloatToStep(offset.y, offsetSnap.y)
            ) : offset;
        }
    }
    public bool enableOffsetSnapping = false;
    public Vector2 offsetSnap = new Vector2(0.05f, 0.05f);
    public bool enableOffsetLimit = false;
    public Vector2 minOffset = new Vector2(1f, 1f);
    public Vector2 maxOffset = new Vector2(0f,0f);

    [Header("Distance")]
    public float _distance = 0f;
    public float distance
    {
        get
        {
            return _distance;
        }
        set
        {
            if (enableDistanceLimit)
            {
                value = Mathf.Clamp(value,minDistance,maxDistance);
            }
            _distance = value;
        }
    }
    public float distanceSnapped
    {
        get
        {
            return enableDistanceSnapping ? MathUtils.roundFloatToStep(distance, distanceSnap) : distance;
        }
    }
    public bool enableDistanceLimit = false;
    public float minDistance = 0f;
    public float maxDistance = 0f;
    public bool enableDistanceSnapping = false;
    public float distanceSnap = 5f;
}
