using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if 4 points are a rectangle or not
/// based on AD.magnitude == BC.magnitude && AB.magnitude = CD.magnitude
/// </summary>
public class RectangleChecker : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Transform C;
    public Transform D;

    public bool isRectangle = false;
    // Update is called once per frame
    void Update()
    {
        Vector3 AD = D.position - A.position;
        Vector3 BC = C.position - B.position;

        Vector3 AB = B.position - A.position;
        Vector3 CD = D.position - C.position;


        isRectangle = Mathf.Abs(AD.magnitude - BC.magnitude) < 0.001f && Mathf.Abs(AB.magnitude - CD.magnitude) < 0.001f;
    }
}
