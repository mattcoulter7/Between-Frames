using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CinematicBarEvents : MonoBehaviour
{
    public UnityEvent onRotate;
    public UnityEvent onMove;
    public UnityEvent onDistance;

    private float _lastDistance;
    private Vector3 _lastOffset;
    private float _lastRotation;

    CinematicBarManager cinematicBarManager;
    // Start is called before the first frame update
    void Start()
    {
        cinematicBarManager = GetComponent<CinematicBarManager>();
        _lastDistance = cinematicBarManager.distanceSnapped;
        _lastOffset = cinematicBarManager.snappedOffset;
        _lastRotation = cinematicBarManager.rotationSnapped;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cinematicBarManager.distanceSnapped;
        Vector3 offset  = cinematicBarManager.snappedOffset;
        float rotation = cinematicBarManager.rotationSnapped;

        if (!distance.Equals(_lastDistance))
        {
            onDistance.Invoke();
        }
        if (!offset.Equals(_lastOffset))
        {
            onMove.Invoke();
        }
        if (!rotation.Equals(_lastRotation))
        {
            onRotate.Invoke();
        }

        _lastDistance = distance;
        _lastOffset = offset;
        _lastRotation = rotation;
    }
}
