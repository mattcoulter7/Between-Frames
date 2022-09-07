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

        public Vector3 GetPoint(Vector2 position,float rotation)
        {
            float xScalar = Mathf.Cos(Mathf.Deg2Rad * rotation);
            float yScalar = Mathf.Sin(Mathf.Deg2Rad * rotation);

            Vector3 AB = B.position - A.position;
            float distanceX = Mathf.Abs(AB.x);
            float distanceY = Mathf.Abs(AB.y);

            float relX = position.x - A.position.x;
            float relY = position.y - A.position.y;

            float scaleFactorX = relX / distanceX;
            float scaleFactorY = relY / distanceY;

            Vector3 newPointX = A.position + AB * scaleFactorX;
            Vector3 newPointY = A.position + AB * scaleFactorY;

            return Time.frameCount % 2 == 0 ? newPointX : newPointY;
        }
    }

    public Transform initialPoint;
    public Line relativeLine;
    public DynamicModifier rotationGetter;

    private void Start()
    {
        rotationGetter.OnInitialise();
    }

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

        float rotation = (float)rotationGetter.GetValue() + 90;
        Debug.Log(rotation);

        // find the intersection between ray Vector3.Forward from initalPoint and relative Line
        transform.position = relativeLine.GetPoint(initialPoint.position, rotation);
    }
}