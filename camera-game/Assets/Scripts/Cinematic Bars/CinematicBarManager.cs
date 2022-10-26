using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This class is responsible for holding all of the variables for the
/// transformations applied to CinematicBar
///
/// This class by itself does not perform any transformations, 
/// but instead they are observed by CinematicBar
/// </summary>
[System.Serializable]
public class CinematicBarManager : MonoBehaviour
{
    [Header("Rotation")]
    /// <summary>The maxRotation if enableRotationLimit is true</summary>
    public float maxRotation = 0f;
    /// <summary>The rounded interval for rotation</summary>
    public float rotationSnap = 5f;
    /// <summary>The raw rotation value</summary>
    [SerializeField] public float rawRotation { get; private set; } = 0f;
    public float snappedRotation { get; private set; }

    [Header("Offset")]
    /// <summary>The minOffset if enableOffsetLimit is true in terms of x and y</summary>
    public Vector2 minOffset = new Vector2(1f, 1f);
    /// <summary>The maxOffset if enableOffsetLimit is true in terms of x and y</summary>
    public Vector2 maxOffset = new Vector2(0f, 0f);
    public float offsetSnap = 0.05f;
    /// <summary>The raw offset value</summary>
    [SerializeField] public Vector2 rawOffset { get; private set; } = new Vector2(0.5f, 0.5f);
    public Vector2 snappedOffset { get; private set; }

    [Header("Distance")]
    /// <summary>The minimum distance value</summary>
    public float minDistance = 0f;
    /// <summary>The maximum distance value</summary>
    public float maxDistance = 0f;
    /// <summary>The rounded interval for distance</summary>
    public float distanceSnap = 5f;
    /// <summary>The raw distance value</summary>
    [SerializeField] public float rawDistance { get; private set; } = 0f;
    /// <summary>The distance as a proportion to the max (used for non-linear scaling)</summary>
    public float normalizedDistance { get; private set; }
    public float snappedDistance { get; private set; }

    /// <summary>Keeps the inside distance the same if true, otherwise keeps the length of the bar from edge of the screen the same</summary>
    public bool maintainPlayableArea = true;


    private CannotKill[] cannotKills;


    private void Start()
    {
        cannotKills = FindObjectsOfType<CannotKill>();
        SetOffset(rawOffset);
        SetDistance(rawDistance);
        SetRotation(rawRotation);
    }

    public void SetOffset(Vector2 value, bool validate = false)
    {
        value.x = Mathf.Clamp(value.x, minOffset.x, maxOffset.x);
        value.y = Mathf.Clamp(value.y, minOffset.y, maxOffset.y);

        Vector2 aspectRatio = GetAspectRatio();
        // Vector2 offsetSnapScalar = new Vector2(offsetSnap, offsetSnap) * aspectRatio;
        Vector2 newSnappedOffset = new Vector2(
            MathUtils.RoundFloatToStep(value.x, offsetSnap),
            MathUtils.RoundFloatToStep(value.y, offsetSnap)
        );

        if (!validate || ValidMove(newSnappedOffset))
        {
            rawOffset = value;
            snappedOffset = newSnappedOffset;
        }
    }

    public void SetRotation(float value, bool validate = false)
    {
        value = Mathf.Clamp(value, -maxRotation, maxRotation);
        float newSnappedRotation = MathUtils.RoundFloatToStep(value, rotationSnap);

        if (!validate || ValidMove(null, newSnappedRotation))
        {
            rawRotation = value;
            snappedRotation = newSnappedRotation;
        }
    }

    public void SetDistance(float value,bool validate = false)
    {
        value = Mathf.Clamp(value, minDistance, maxDistance);
        float newSnappedDistance = MathUtils.RoundFloatToStep(value, distanceSnap);

        if (!validate || ValidMove(null, null, newSnappedDistance))
        {
            rawDistance = value;
            snappedDistance = newSnappedDistance;
            normalizedDistance = (value - minDistance) / (maxDistance - minDistance);
        }
    }

    private bool ValidMove(Vector2? newSnappedOffset = null,float? newSnappedRotation = null, float? newSnappedDistance = null)
    {
        if (newSnappedOffset.HasValue)
        {
        }
        else if (newSnappedRotation.HasValue)
        {
        }
        else if (newSnappedDistance.HasValue)
        {
        }
        return true;
    }

    private Vector2 GetAspectRatio()
    {
        return maintainPlayableArea ? new Vector2(
            Screen.height,
            Screen.width
        ).normalized : new Vector2(1, 1);
    }
}
