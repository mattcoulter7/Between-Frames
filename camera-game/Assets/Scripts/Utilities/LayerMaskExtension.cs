using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskExtension
{
    public static bool HasLayer(this LayerMask layerMask, int layer)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
