using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : FollowTransform
{
    void Update()
    {
        Vector3 sum = target.position + offset;
        transform.position = new Vector3(
            (xMultiplier != 0f) ? sum.x * xMultiplier : transform.position.x,
            (yMultiplier != 0f) ? sum.y * yMultiplier : transform.position.y,
            (zMultiplier != 0f) ? sum.z * zMultiplier : transform.position.z
        );
    }
}
