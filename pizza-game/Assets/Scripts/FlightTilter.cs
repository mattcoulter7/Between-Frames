using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightTilter : MonoBehaviour
{
    public float stability = 0.3f;
    public float speed = 2.0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // add direction tilt
        var locVel = transform.InverseTransformDirection(rb.velocity);
        Vector3 currentRotation = transform.rotation.eulerAngles;

        Vector3 rotation = new Vector3(
            locVel.z - currentRotation.x,
            0,
            -locVel.x - currentRotation.z
        );

        transform.Rotate(rotation);
    }
}
