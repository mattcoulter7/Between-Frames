using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

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

    /// <summary>The speed scale for updating CinematicBarManager rotation</summary>
    public float rotateSpeed = 1f;
    
    /// <summary>The speed scale for updating CinematicBarManager offset</summary>
    public float moveSpeed = 0.1f;
    public float controllerZoomSpeed = 0.1f;
    public float controllerRotateSpeed = 1.8f;
    public float controllerMoveSpeed = 1f;

    private CinematicBarManager _cinematicBars;

    // new input
    private PlayerInput playerInput;
    private InputAction shrinkAct;
    private InputAction expandAct;
    private InputAction rotate;
    private InputAction rotateRAct;
    private InputAction rotateLAct;
    private InputAction shiftCamXY;
    private InputAction shiftCamXYMouse;
    private InputAction mouseMovement;

    private bool isShrinking = false;
    private bool isExpanding = false;
    private bool isRotatingR = false;
    private bool isRotatingL = false;
    private bool isRotatingMouse = false;
    private bool isShiftingMouse = false;

    private float zoomFrame = 0f;
    private Vector2 shiftFrame = new Vector2(0f,0f);
    private float rotateFrame = 0;

    private Vector2 mouseDelta;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _cinematicBars = GetComponent<CinematicBarManager>();


        playerInput = FindObjectOfType<PlayerInput>();

        //mouseZoom = 
        shrinkAct = playerInput.actions["Shrink"];
        expandAct = playerInput.actions["Expand"];
        rotate = playerInput.actions["Rotate"];
        rotateRAct = playerInput.actions["RotateRight"];
        rotateLAct = playerInput.actions["RotateLeft"];
        shiftCamXY = playerInput.actions["ShiftCamXY"];
        shiftCamXYMouse = playerInput.actions["ShiftCamXYMouse"];
        mouseMovement = playerInput.actions["MouseMovement"];

        shrinkAct.performed += _ => isShrinking = true;
        shrinkAct.canceled += _ => isShrinking = false;

        expandAct.performed += _ => isExpanding = true;
        expandAct.canceled += _ => isExpanding = false;

        rotateRAct.performed += _ => isRotatingR = true;
        rotateRAct.canceled += _ => isRotatingR = false;

        rotateLAct.performed += _ => isRotatingL = true;
        rotateLAct.canceled += _ => isRotatingL = false;

        rotate.performed += _ => isRotatingMouse = true;
        rotate.canceled += _ => isRotatingMouse = false;

        shiftCamXYMouse.performed += _ => isShiftingMouse = true;
        shiftCamXYMouse.canceled += _ => isShiftingMouse = false;

        mouseMovement.performed += _ => mouseDelta = _.ReadValue<Vector2>();
        mouseMovement.canceled += _ => mouseDelta = _.ReadValue<Vector2>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleDistance();
        HandleRotation();
        HandleOffset();
    }

    private void HandleDistance()
    {
        zoomFrame = playerInput.actions["MouseZoom"].ReadValue<Vector2>().normalized.y;

        if (isShrinking) // shrink
        {
            zoomFrame -= controllerZoomSpeed * Time.deltaTime;
        }

        if (isExpanding) // expand
        {
            zoomFrame += controllerZoomSpeed * Time.deltaTime;
        }
        // handle zooming to change distance
        if (enableZoom && zoomFrame != 0f)
        {
            float delta = zoomFrame * GetScaledZoomSpeed();
            _cinematicBars.SetDistance(_cinematicBars.rawDistance + delta);
        }
    }

    private void HandleRotation()
    {
        rotateFrame = 0f;

        if (isRotatingR) // Right
        {
            rotateFrame -= controllerRotateSpeed * Time.deltaTime;
        }

        if (isRotatingL) // Left
        {
            rotateFrame += controllerRotateSpeed * Time.deltaTime;
        }

        if (isRotatingMouse)
        {
            rotateFrame += GetCircularMotion();
        }

        // move mouse right whilst holding left mouse button to rotate
        if (enableRotation && rotateFrame != 0)
        {
            float delta = rotateFrame * rotateSpeed;
            _cinematicBars.SetRotation(_cinematicBars.rawRotation + delta);
        }
    }

    private void HandleOffset()
    {
        // reset properties
        shiftFrame = Vector2.zero;


        if (shiftCamXY.ReadValue<Vector2>() != Vector2.zero)
        {
            shiftFrame = shiftCamXY.ReadValue<Vector2>() * controllerMoveSpeed * Time.deltaTime;
        }

        if (isShiftingMouse)
        {
            shiftFrame -= mouseDelta;
        }

        // click and drag left mouse button to move origin
        if (enablePan && shiftFrame.magnitude != 0)
        {
            Vector2 delta = Camera.main.ScreenToViewportPoint(shiftFrame * moveSpeed);
            _cinematicBars.SetOffset(_cinematicBars.rawOffset - delta);
        }
    }

    private Vector2 GetMouseSegment()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 viewportMousePosition = Camera.main.ScreenToViewportPoint(mousePosition);
        Vector2 middle = _cinematicBars.rawOffset;

        Vector2 segment = Vector2.zero;
        segment.x = viewportMousePosition.x > middle.x ? 1 : 0;
        segment.y = viewportMousePosition.y > middle.y ? 1 : 0;

        return segment;
    }

    private float GetCircularMotion()
    {
        Vector2 mouseSegment = GetMouseSegment();

        // < 0: clockwise, > 0: anticlockwise
        float rotation = 0f;

        // METHOD 1: only care about the more significant direction
        // *Top Left[0, 1](Clockwise: Right / Up, Counterclockwise: Left / Down)
        if (mouseSegment.Equals(new Vector2(0, 1)))
        {
            rotation -= mouseDelta.x;
            rotation -= mouseDelta.y;
        }
        // *Top Right[1, 1](Clockwise: Right / Down, Counterclockwise: Left / Up)
        else if (mouseSegment.Equals(new Vector2(1, 1)))
        {
            rotation -= mouseDelta.x;
            rotation += mouseDelta.y;
        }
        // *Bottom Left[0, 0](Clockwise: Left / Up, Counterclockwise: Right / Down)
        else if (mouseSegment.Equals(new Vector2(0, 0)))
        {
            rotation += mouseDelta.x;
            rotation -= mouseDelta.y;
        }
        // *Bottom Right[1, 0](Clockwise: Left / Down, Counterclockwise: Right / Up)
        else if (mouseSegment.Equals(new Vector2(1, 0)))
        {
            rotation += mouseDelta.x;
            rotation += mouseDelta.y;
        }

        return rotation;
    }

    /// <summary>The non-linear calculation of the zoom amount</summary>
    public float GetScaledZoomSpeed()
    {
        return zoomSpeed * zoomSpeedCurve.Evaluate(_cinematicBars.normalizedDistance);
    }
}
