using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardPointOnLine : MonoBehaviour
{
    [System.Serializable]
    public class Line
    {
        public Transform A;
        public Transform B;

        public Vector3 GetPoint(float x)
        {
            Vector3 AB = B.position - A.position;
            float distance = Mathf.Abs(AB.x);

            float relX = x - A.position.x;

            float scaleFactor = relX / distance;

            Vector3 newPoint = A.position + AB * scaleFactor;

            return newPoint;
        }
    }

    public Transform initialPoint;
    public Line relativeLine;

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

        // find the intersection between ray Vector3.Forward from initalPoint and relative Line
        transform.position = relativeLine.GetPoint(initialPoint.position.x);
    }
}