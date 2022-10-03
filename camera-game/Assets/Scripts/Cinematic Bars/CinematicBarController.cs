using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for updating variables of the CinematicBarManager based on user controls
/// </sumary>
public class CinematicBarController : MonoBehaviour
{
    /// <summary>CinematicBarManager distance can be controlled if true</summary>
    public bool enableZoom = true;
    
    /// <summary>CinematicBarManager offset can be controlled if true</summary>
    public bool enablePan = true;
    
    /// <summary>CinematicBarManager rotation can be controlled if true</summary>
    public bool enableRotation = true;
    
    /// <summary>The speed scale for updating CinematicBarManager distance</summary>
    public float zoomSpeed = 1f;
    
    /// <summary>This curve enables the zooming to become non-linear</summary>
    public AnimationCurve zoomSpeedCurve;
    
    /// <summary>The non-linear calculation of the zoom amount</summary>
    public float scaledZoomSpeed
    {
        get
        {
            return zoomSpeed * zoomSpeedCurve.Evaluate(_cinematicBars.normalizedDistance);
        }
    }

    /// <summary>The speed scale for updating CinematicBarManager rotation</summary>
    public float rotateSpeed = 1f;
    
    /// <summary>The speed scale for updating CinematicBarManager offset</summary>
    public float moveSpeed = 0.1f;
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

    private Vector2 mousePosition;
    private Vector2 lastMousePosition;
    private Vector2 mouseMovement;


    // Start is called before the first frame update
    private void Start()
    {
        _cinematicBars = GetComponent<CinematicBarManager>();

        if (playerInput == null)
        {
            playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        }

        //mouseZoom = 
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

    private Vector2 GetMouseSegment()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 viewportMousePosition = Camera.main.ScreenToViewportPoint(mousePosition);
        Vector2 middle = _cinematicBars.offsetSnapped;

        Vector2 segment = Vector2.zero;
        segment.x = viewportMousePosition.x > middle.x ? 1 : 0;
        segment.y = viewportMousePosition.y > middle.y ? 1 : 0;

        return segment;
    }


    private float GetCircularMotion()
    {
        Vector2 mouseSegment = GetMouseSegment();
        Vector2 rotationInput = mouseMovement;

        // < 0: clockwise, > 0: anticlockwise
        float rotation = 0f;

        // METHOD 1: only care about the more significant direction
        // *Top Left[0, 1](Clockwise: Right / Up, Counterclockwise: Left / Down)
        if (mouseSegment.Equals(new Vector2(0,1)))
        {
            rotation -= rotationInput.x;
            rotation -= rotationInput.y;
        }
        // *Top Right[1, 1](Clockwise: Right / Down, Counterclockwise: Left / Up)
        else if (mouseSegment.Equals(new Vector2(1,1)))
        {
            rotation -= rotationInput.x;
            rotation += rotationInput.y;
        }
        // *Bottom Left[0, 0](Clockwise: Left / Up, Counterclockwise: Right / Down)
        else if (mouseSegment.Equals(new Vector2(0,0)))
        {
            rotation += rotationInput.x;
            rotation -= rotationInput.y;
        }
        // *Bottom Right[1, 0](Clockwise: Left / Down, Counterclockwise: Right / Up)
        else if (mouseSegment.Equals(new Vector2(1,0)))
        {
            rotation += rotationInput.x;
            rotation += rotationInput.y;
        }

        return rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        float zoomFrame = playerInput.actions["MouseZoom"].ReadValue<Vector2>().normalized.y;

        lastMousePosition = mousePosition != null ? mousePosition : Input.mousePosition; // initial frame default to mouse position
        mousePosition = Input.mousePosition;
        mouseMovement = mousePosition - lastMousePosition;

        //float zoomFrame = Input.GetAxis("ZoomFrame");
        float shiftFrameX = Input.GetButton("ShiftFrameX") ? Input.GetAxis("ShiftFrameX") : 0;
        float shiftFrameY = Input.GetButton("ShiftFrameY") ? Input.GetAxis("ShiftFrameY") : 0;
        Vector2 shiftFrame = new Vector2(shiftFrameX, shiftFrameY);
        float rotateFrame = Input.GetButton("RotateFrame") ? GetCircularMotion() : 0;

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
            _cinematicBars.distance += zoomFrame * scaledZoomSpeed * Time.deltaTime;
        }


        // click and drag left mouse button to move origin
        if (enablePan && shiftFrame != Vector2.zero)
        {
            _cinematicBars.offset -= (Vector2)Camera.main.ScreenToViewportPoint(shiftFrame * moveSpeed * Time.deltaTime); ;
        }

        // move mouse right whilst holding left mouse button to rotate
        if (enableRotation && rotateFrame != 0)
        {
            _cinematicBars.rotation += rotateFrame * rotateSpeed * Time.deltaTime;
        }
    }
}
