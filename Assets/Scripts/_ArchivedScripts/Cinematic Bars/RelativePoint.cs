using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePoint : MonoBehaviour
{
    [System.Serializable]
    public struct Line
    {
        public Transform A;
        public Transform B;
    }

    public Transform initialPoint;
    public Line relativeLine;
    public bool inverse = false;

    // Update is called once per frame
    void Update()
    {
        // A should be the farther left x position
        if (relativeLine.A.position.x > relativeLine.B.position.x)
        {
            Transform tempA = relativeLine.A;
            relativeLine.A = relativeLine.B;
            relativeLine.B = tempA;
        }
        Vector3 AB = relativeLine.B.position - relativeLine.A.position;
        float distance = Mathf.Abs(AB.x);

        float relativeX = initialPoint.position.x - relativeLine.A.position.x;

        float scaleFactor = relativeX / distance;

        if (inverse) scaleFactor = 1 - scaleFactor;

        Vector3 newPoint = relativeLine.A.position + AB.normalized * scaleFactor * distance;

        transform.position = newPoint;
    }
}
