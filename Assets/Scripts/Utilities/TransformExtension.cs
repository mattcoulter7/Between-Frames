using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static string GetPath(this Transform current)
    {
        if (current.parent == null)
            return "/" + current.name;
        return current.parent.GetPath() + "/" + current.name;
    }
    public static void SetPosition(this Transform t, Vector3 position) => t.position = position;
    public static void SetRotation(this Transform t, Quaternion rotation) => t.rotation = rotation;
    public static void SetScale(this Transform t, Vector3 scale) => t.localScale = scale;
}
