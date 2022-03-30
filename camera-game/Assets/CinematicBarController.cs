using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarController : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float rotateSpeed = 1f;
    private CinematicBars _cinematicBars;
    private Vector3 _lastMousePos = new Vector3(0f,0f,0f);

    private Vector3 _mousePos = new Vector3(0f,0f,0f);
    private Vector3 _mouseMovement = new Vector3(0f,0f,0f);
    // Start is called before the first frame update
    void Start()
    {
        _cinematicBars = GetComponent<CinematicBars>();
    }

    CinematicBar GetMouseInteractCinematicBar(){
        CinematicBar cinematicBar = null;
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray,out RaycastHit hitInfo)){
            CinematicBar barComponent = hitInfo.collider.gameObject.GetComponent<CinematicBar>();
            if (barComponent){
                cinematicBar = barComponent;
            }
        }
        return cinematicBar;
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse pos
        _mousePos = Input.mousePosition;
        _mouseMovement = _lastMousePos - _mousePos;
        // handle zooming to change distance
        
        float scrollAmount = Input.mouseScrollDelta.y;
        if (scrollAmount != 0f){
            // find object mouse is intersecting
            CinematicBar bar = GetMouseInteractCinematicBar();
            if (bar == null){
                // if it is one of the black bars, chane that instead of both
                _cinematicBars.distance += scrollAmount * zoomSpeed;
            } else {
                bar.distanceOffset += scrollAmount * zoomSpeed;
            }
        }

        // click to move origin
        if (Input.GetMouseButton(0)){
            _cinematicBars.offset -= (Vector2)Camera.main.ScreenToViewportPoint(_mouseMovement);;
        }

        // move mouse right whilst holding left mouse button to rotate
        if (Input.GetMouseButton(1)){
            _cinematicBars.rotation += _mouseMovement.x * rotateSpeed;
        }

        // track last mouse position
        _lastMousePos = _mousePos;
    }
}
