using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarController : MonoBehaviour
{
    public bool enableZoom = true;
    public bool enableIndividualZoom = true;
    public bool enablePan = true;
    public bool enableRotation = true;
    public float zoomSpeed = 1f;
    public AnimationCurve zoomSpeedCurve;
    public float scaledZoomSpeed
    {
        get
        {
            return zoomSpeed * zoomSpeedCurve.Evaluate(_cinematicBars.normalizedDistance);
        }
    }
    public float rotateSpeed = 1f;
    public float moveSpeed = 1f;
    private CinematicBarManager _cinematicBars;
    // Start is called before the first frame update
    void Start()
    {
        _cinematicBars = GetComponent<CinematicBarManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float zoomFrame = Input.GetAxis("ZoomFrame");
        float shiftFrameX = Input.GetButton("ShiftFrameX") ? Input.GetAxis("ShiftFrameX") : 0;
        float shiftFrameY = Input.GetButton("ShiftFrameY") ? Input.GetAxis("ShiftFrameY") : 0;
        Vector2 shiftFrame = new Vector2(shiftFrameX, shiftFrameY);
        float rotateFrame = Input.GetButton("RotateFrame") ? Input.GetAxis("RotateFrame") : 0;

        // handle zooming to change distance
        if (enableZoom && zoomFrame != 0f)
        {
            // if it is one of the black bars, chane that instead of both
            _cinematicBars.distance += zoomFrame * scaledZoomSpeed;
        }

        // click and drag left mouse button to move origin
        if (enablePan && shiftFrame != Vector2.zero)
        {
            _cinematicBars.offset -= (Vector2)Camera.main.ScreenToViewportPoint(shiftFrame * moveSpeed); ;
        }

        // move mouse right whilst holding left mouse button to rotate
        if (enableRotation && rotateFrame != 0)
        {
            _cinematicBars.rotation += rotateFrame * rotateSpeed;
        }
    }
}
