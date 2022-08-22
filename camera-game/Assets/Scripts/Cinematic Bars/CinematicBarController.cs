using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public float rotateSpeed = 1.0f;
    public float moveSpeed = 1f;
    public float controllerZoomSpeed = 0.1f;
    public float controllerRotationSpeed = 1.8f;
    private CinematicBarManager _cinematicBars;

    // new input
    private PlayerInput playerInput;
    private InputAction shrinkAct;
    private InputAction expandAct;
    private InputAction rotateRAct;
    private InputAction rotateLAct;
    private InputAction ShiftCamXY;



    private bool isShrinking = false;
    private bool isExpanding = false;
    private bool isRotatingR = false;
    private bool isRotatingL = false;


    // Start is called before the first frame update
    void Start()
    {
        _cinematicBars = GetComponent<CinematicBarManager>();

        if (playerInput == null)
        {
            playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        }

        shrinkAct = playerInput.actions["Shrink"];
        expandAct = playerInput.actions["Expand"];
        rotateRAct = playerInput.actions["RotateRight"];
        rotateLAct = playerInput.actions["RotateLeft"];
        ShiftCamXY = playerInput.actions["ShiftCamXY"];

        shrinkAct.performed += _ => isShrinking = true;
        shrinkAct.canceled += _ => isShrinking = false;

        expandAct.performed += _ => isExpanding = true;
        expandAct.canceled += _ => isExpanding = false;

        rotateRAct.performed += _ => isRotatingR = true;
        rotateRAct.canceled += _ => isRotatingR = false;

        rotateLAct.performed += _ => isRotatingL = true;
        rotateLAct.canceled += _ => isRotatingL = false;
    }

    // Update is called once per frame
    void Update()
    {
        float zoomFrame = Input.GetAxis("ZoomFrame");
        float shiftFrameX = Input.GetButton("ShiftFrameX") ? Input.GetAxis("ShiftFrameX") : 0;
        float shiftFrameY = Input.GetButton("ShiftFrameY") ? Input.GetAxis("ShiftFrameY") : 0;
        Vector2 shiftFrame = new Vector2(shiftFrameX, shiftFrameY);
        float rotateFrame = Input.GetButton("RotateFrame") ? Input.GetAxis("RotateFrame") : 0;

        if (isShrinking) // shrink
        {
            zoomFrame -= controllerZoomSpeed;
        }

        if (isExpanding) // expand
        {
            zoomFrame += controllerZoomSpeed;
        }

        if(isRotatingR) // Right
        {
            rotateFrame -= controllerRotationSpeed;
        }

        if (isRotatingL) // Left
        {
            rotateFrame += controllerRotationSpeed;
        }

        if(ShiftCamXY.ReadValue<Vector2>() != Vector2.zero)
        {
            shiftFrame = ShiftCamXY.ReadValue<Vector2>();
        }

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
