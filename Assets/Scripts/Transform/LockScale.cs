using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScale : LockTransform
{
    void Update()
    {
        transform.localScale = new Vector3(
            x ? vector.x : transform.localScale.x,
            y ? vector.y : transform.localScale.y,
            z ? vector.z : transform.localScale.z
        );
    }
}
