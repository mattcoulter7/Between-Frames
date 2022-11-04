using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is responsible for calling UnityEvents for when changes in
/// CinematicBarManagers rotation, move and zoom
/// </summary>
public class CinematicBarEvents : MonoBehaviour
{

    /// <summary>The configured event for what happens when the rotation changes</summary>
    public UnityEvent onRotate;
    
    /// <summary>The configured event for what happens when the offset changes</summary>
    public UnityEvent onMove;
    
    /// <summary>The configured event for what happens when the distance changes</summary>
    public UnityEvent onDistance;

    private float _lastDistance;
    private Vector3 _lastOffset;
    private float _lastRotation;

    private CinematicBarManager cinematicBarManager;
    // Start is called before the first frame update
    private void Start()
    {
        cinematicBarManager = GetComponent<CinematicBarManager>();
        _lastDistance = cinematicBarManager.distanceSnapped;
        _lastOffset = cinematicBarManager.offsetSnapped;
        _lastRotation = cinematicBarManager.rotationSnapped;
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = cinematicBarManager.distance;
        Vector3 offset  = cinematicBarManager.offset;
        float rotation = cinematicBarManager.rotation;

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
