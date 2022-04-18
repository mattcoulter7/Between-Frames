using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotation : FollowTransform
{
    void Update()
    {
        Vector3 sum = target.eulerAngles + offset;
        transform.rotation = Quaternion.Euler(
            (xMultiplier != 0f) ? sum.x * xMultiplier : transform.eulerAngles.x,
            (yMultiplier != 0f) ? sum.y * yMultiplier : transform.eulerAngles.y,
            (zMultiplier != 0f) ? sum.z * zMultiplier : transform.eulerAngles.z
        );
    }
}
