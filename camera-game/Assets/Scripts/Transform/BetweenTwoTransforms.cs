using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenTwoTransforms : MonoBehaviour
{
    public Transform transform1;
    public Transform transform2;
    public Vector3 span {
        get {
            return transform2.position - transform1.position;
        }
    }

    public float displacement = 0.5f;

    void Reposition()
    {
        transform.position = transform1.position + (span * displacement);
    }

    private void Start()
    {
        Reposition();
    }
    // Update is called once per frame
    void Update()
    {
        Reposition();
    }
}
