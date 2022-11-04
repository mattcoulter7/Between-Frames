using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : LockTransform
{
    void Update()
    {
        transform.position = new Vector3(
            x ? vector.x : transform.position.x,
            y ? vector.y : transform.position.y,
            z ? vector.z : transform.position.z
        );
    }
}
