using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for controlling point position of a Bone Weighted mesh
/// </summary>
public class BoneWeightedBoxController : MonoBehaviour
{
    /// <summary>The Transform component reference of Bone Weight Bottom Left Back</summary>
    public Transform bottomLeftBack;

    /// <summary>The Transform component reference of Bone Weight Bottom Right Back</summary>
    public Transform bottomRightBack;

    /// <summary>The Transform component reference of Bone Weight Bottom Left Front</summary>
    public Transform bottomLeftFront;

    /// <summary>The Transform component reference of Bone Weight Bottom Right Front</summary>
    public Transform bottomRightFront;

    /// <summary>The Transform component reference of Bone Weight Top Left Back</summary>
    public Transform topLeftBack;

    /// <summary>The Transform component reference of Bone Weight Top Right Back</summary>
    public Transform topRightBack;

    /// <summary>The Transform component reference of Bone Weight Top Left Front</summary>
    public Transform topLeftFront;

    /// <summary>The Transform component reference of Bone Weight Top Right Front</summary>
    public Transform topRightFront;

    /// <summary>
    /// Enum used for grouping points together
    /// </summary>
    public enum PointGroup
    {
        LEFT,
        BOTTOM,
        RIGHT,
        TOP,
        BACK,
        FRONT,
        LEFT_BOTTOM,
        RIGHT_BOTTOM,
    }
    private Dictionary<PointGroup, Transform[]> pointGroups = new Dictionary<PointGroup, Transform[]>() { };
    
    /// <summary>
    /// Moves a single point on the bone weighted mesh by an amount
    /// </summary>
    /// <param name="point">The Transform bone</param>
    /// <param name="amount">The Vector3 offset amount (summed with the current position)</param>
    public void ShiftPoint(Transform point, Vector3 amount)
    {
        point.position += amount;
    }

    /// <summary>
    /// Moves multiple points on the bone weighted mesh by the same amount
    /// </summary>
    /// <param name="points">The Array of Transform bones</param>
    /// <param name="amount">The Vector3 offset amount (summed with the current position of each bone)</param>
    public void ShiftPoints(Transform[] points, Vector3 amount)
    {
        foreach (Transform pt in points)
        {
            ShiftPoint(pt, amount);
        }
    }
    
    /// <summary>
    /// Moves an entire group of points on the bone weighted mesh by the same amount
    /// </summary>
    /// <param name="points">The PointGroup face</param>
    /// <param name="amount">The Vector3 offset amount (summed with the current position of each bone)</param>
    public void ShiftGroup(PointGroup group, Vector3 amount)
    {
        ShiftPoints(pointGroups[group], amount);
    }
    protected virtual void Start()
    {
        // define all of the relevant point groups
        pointGroups.Add(PointGroup.LEFT, new Transform[4] { bottomLeftBack, bottomLeftFront, topLeftBack, topLeftFront });
        pointGroups.Add(PointGroup.BOTTOM, new Transform[4] { bottomLeftBack, bottomRightBack, bottomRightFront, bottomLeftFront });
        pointGroups.Add(PointGroup.RIGHT, new Transform[4] { bottomRightBack, bottomRightFront, topRightBack, topRightFront });
        pointGroups.Add(PointGroup.TOP, new Transform[4] { topLeftBack, topRightBack, topRightFront, topLeftFront });
        pointGroups.Add(PointGroup.BACK, new Transform[4] { bottomLeftBack, bottomRightBack, topLeftBack, topRightBack });
        pointGroups.Add(PointGroup.FRONT, new Transform[4] { bottomRightFront, bottomLeftFront, topRightFront, topLeftFront });
        pointGroups.Add(PointGroup.LEFT_BOTTOM, new Transform[2] { bottomLeftFront, bottomLeftBack });
        pointGroups.Add(PointGroup.RIGHT_BOTTOM, new Transform[2] { bottomRightFront, bottomRightBack });
    }
}
