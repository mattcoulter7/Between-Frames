using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// This class is responsible for holding all of the variables for the
/// transformations applied to CinematicBar
///
/// This class by itself does not perform any transformations, 
/// but instead they are observed by CinematicBar
/// </summary>
public class CinematicBarManager : MonoBehaviour
{
    [Header("Rotation")]
    /// <summary>The raw rotation value</summary>
    public float _rotation = 0f;

    /// <summary>The _rotation after it has been limited by min and max</summary>
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

    /// <summary>The rotation after it has been snapped by rotationSnap</summary>
    public float rotationSnapped
    {
        get
        {
            return enableRotationSnapping ? MathUtils.roundFloatToStep(rotation, rotationSnap) : rotation;
        }
    }

    /// <summary>Rounds the rotation to an interval if true</summary>
    public bool enableRotationSnapping = false;

    /// <summary>The rounded interval for rotation</summary>
    public float rotationSnap = 5f;

    /// <summary>Clamps the rotation -maxRotation and maxRotation if true</summary>
    public bool enableRotationLimit = false;

    /// <summary>The maxRotation if enableRotationLimit is true</summary>
    public float maxRotation = 0f;

    [Header("Offset")]
    /// <summary>The raw offset value</summary>
    public Vector2 _offset = new Vector2(0.5f, 0.5f);
    
    /// <summary>The _offset after it has been limited by min and max</summary>
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
    
    /// <summary>The offset after it has been snapped by offsetSnap</summary>
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

    /// <summary>Rounds the offset to an interval if true</summary>
    public bool enableOffsetSnapping = false;

    /// <summary>The rounded interval for offset in terms of x and y</summary>
    public Vector2 offsetSnap = new Vector2(0.05f, 0.05f);

    /// <summary>Clamps the rotation minOffset and maxOffset if true in terms of x and y</summary>
    public bool enableOffsetLimit = false;
    
    /// <summary>The minOffset if enableOffsetLimit is true in terms of x and y</summary>
    public Vector2 minOffset = new Vector2(1f, 1f);

    /// <summary>The maxOffset if enableOffsetLimit is true in terms of x and y</summary>
    public Vector2 maxOffset = new Vector2(0f,0f);

    [Header("Distance")]
    /// <summary>The raw distance value</summary>
    public float _distance = 0f;
    
    /// <summary>The _distance after it has been limited by minDistance and maxDistance</summary>
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
    
    /// <summary>The distance as a proportion to the max (used for non-linear scaling)</summary>
    public float normalizedDistance
    {
        get
        {
            return enableDistanceLimit ? (distance - minDistance) / (maxDistance - minDistance) : 0;
        }
    }
    
    /// <summary>The distance after it has been snapped by distanceSnap</summary>
    public float distanceSnapped
    {
        get
        {
            return enableDistanceSnapping ? MathUtils.roundFloatToStep(distance, distanceSnap) : distance;
        }
    }
    
    /// <summary>Clamps the distance minDistance and maxDistance if true</summary>
    public bool enableDistanceLimit = false;
    
    /// <summary>The minimum distance value</summary>
    public float minDistance = 0f;
    
    /// <summary>The maximum distance value</summary>
    public float maxDistance = 0f;
    
    /// <summary>Rounds the distance to distanceSnap if true</summary>
    public bool enableDistanceSnapping = false;
    
    /// <summary>The rounded interval for distance</summary>
    public float distanceSnap = 5f;

    /// <summary>Setter for offset (dedicated helped function for Unity inspector binding)</summary>
    /// <param name="value">The new value</param>
    public void SetOffset(Vector2 value) => offset = value;

    /// <summary>Setter for distance (dedicated helped function for Unity inspector binding)</summary>
    /// <param name="value">The new value</param>
    public void SetDistance(float value) => distance = value;

    /// <summary>Setter for rotation (dedicated helped function for Unity inspector binding)</summary>
    /// <param name="value">The new value</param>
    public void SetRotation(float value) => rotation = value;
}
