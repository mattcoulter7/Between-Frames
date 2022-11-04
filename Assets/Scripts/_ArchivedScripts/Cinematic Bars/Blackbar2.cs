using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackbar2 : MonoBehaviour
{
    public float distanceScale = 1f;
    private float lastZRotation;
    // Start is called before the first frame update
    void Start()
    {
        lastZRotation = transform.parent.rotation.z;
    }

    void OnRotation(){
        Vector3 toObject = (transform.position - transform.parent.position).normalized;

        // get offset position from origin
        Vector3 pivotPoint = transform.parent.position;
        Vector3 viewportPivotPoint = Camera.main.WorldToViewportPoint(pivotPoint);

        Vector3 targetViewportPosition = viewportPivotPoint + toObject * distanceScale;
        Vector3 targetWorldPosition = Camera.main.ViewportToWorldPoint(targetViewportPosition);
        transform.position = targetWorldPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.rotation.z != lastZRotation){
            OnRotation();
        }

        lastZRotation = transform.parent.rotation.z;
    }
}
