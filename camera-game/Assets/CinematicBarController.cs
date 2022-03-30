using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarController : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float rotateSpeed = 1f;
    private CinematicBars _cinematicBars;
    private Vector3 _lastMousePos = new Vector3(0f,0f,0f);
    // Start is called before the first frame update
    void Start()
    {
        _cinematicBars = GetComponent<CinematicBars>();
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse pos
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseMovement = _lastMousePos - mousePos;
        // handle zooming to change distance
        float scrollAmount = Input.mouseScrollDelta.y;
        _cinematicBars.distance += scrollAmount * zoomSpeed;

        // click to move origin
        if (Input.GetMouseButton(0)){
            _cinematicBars.offset -= (Vector2)Camera.main.ScreenToViewportPoint(mouseMovement);;
        }

        // move mouse right whilst holding left mouse button to rotate
        if (Input.GetMouseButton(1)){
            _cinematicBars.rotation += mouseMovement.x * rotateSpeed;
        }

        // track last mouse position
        _lastMousePos = mousePos;
    }
}
