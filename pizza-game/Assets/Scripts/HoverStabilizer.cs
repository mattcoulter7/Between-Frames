using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverStabilizer : MonoBehaviour
{
    public float stability = 0.3f;
    public float speed = 2.0f;
    Rigidbody rb;
    // Update is called once per frame
    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector3 predictedUp = Quaternion.AngleAxis(
            rb.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
            rb.angularVelocity
        ) * transform.up;
        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        rb.AddTorque(torqueVector * speed * speed);
    }
}
