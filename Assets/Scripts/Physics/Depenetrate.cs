using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Depenetrate : MonoBehaviour
{
    public float maxDepenetrationVelocity = 50f;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.maxDepenetrationVelocity = maxDepenetrationVelocity;
    }
}
