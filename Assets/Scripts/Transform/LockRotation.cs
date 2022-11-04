using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : LockTransform
{

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(
            x ? vector.x : transform.rotation.x,
            y ? vector.y : transform.rotation.y,
            z ? vector.z : transform.rotation.z
        );
    }
}
