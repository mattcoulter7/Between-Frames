using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class FollowTransform : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0,0,0);
    public float xMultiplier = 1f;
    public float yMultiplier = 1f;
    public float zMultiplier = 1f;

    protected abstract void Follow();
    private void Update()
    {
        Follow();
    }
    private void Start()
    {
        Follow();
    }
}
