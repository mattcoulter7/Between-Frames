using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScale : FollowTransform
{

    void Update()
    {
        Vector3 sum = target.localScale + offset;
        transform.localScale = new Vector3(
            (xMultiplier != 0f) ? sum.x * xMultiplier : transform.localScale.x,
            (yMultiplier != 0f) ? sum.y * yMultiplier : transform.localScale.y,
            (zMultiplier != 0f) ? sum.z * zMultiplier : transform.localScale.z
        );
    }
}
