using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneWeightedBoxController : MonoBehaviour
{
    public Transform bottomLeftBack;
    public Transform bottomRightBack;
    public Transform bottomLeftFront;
    public Transform bottomRightFront;
    public Transform topLeftBack;
    public Transform topRightBack;
    public Transform topLeftFront;
    public Transform topRightFront;
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
    public void ShiftPoint(Transform point, Vector3 amount)
    {
        point.position += amount;
    }

    public void ShiftPoints(Transform[] points, Vector3 amount)
    {
        foreach (Transform pt in points)
        {
            ShiftPoint(pt, amount);
        }
    }
    public void ShiftGroup(PointGroup group, Vector3 amount)
    {
        ShiftPoints(pointGroups[group], amount);
    }
    void Start()
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
