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
    
    /// <summary>The movespeed scale for updating CinematicBarManager offset on mouse</summary>
    public float moveSpeed = 0.1f;

    /// <summary>The movespeed scale for updating CinematicBarManager zoomspeed on controller</summary>
    public float controllerZoomSpeed = 0.1f;

    /// <summary>The rotation speed scale for updating CinematicBarManager rotation on controller</summary>
    public float controllerRotateSpeed = 1.8f;

    /// <summary>The movespeed scale for updating CinematicBarManager offset on controller</summary>
    public float controllerMoveSpeed = 1f;

    /// <summary>Reference to CinematicBarManager Class</summary>
    private CinematicBarManager _cinematicBars;

    /// <summary>InputAction Related variables</summary>  

    /// <summary>PlayerInput class reference </summary>
    private PlayerInput playerInput;

    /// <summary>PlayerInput Action reference </summary>
    private InputAction shrinkAct;

    /// <summary>InputAction Related variables</summary>
    private InputAction expandAct;

    /// <summary>InputAction Related variables</summary>
    private InputAction rotate;

    /// <summary>InputAction Related variables</summary>
    private InputAction rotateRAct;

    /// <summary>InputAction Related variables</summary>
    private InputAction rotateLAct;

    /// <summary>InputAction Related variables</summary>
    private InputAction shiftCamXY;

    /// <summary>InputAction Related variables</summary>
    private InputAction shiftCamXYMouse;

    /// <summary>InputAction Related variables</summary>
    private InputAction mouseMovement;


    /// <summary>Bool check if shrinking input is being pressed</summary>
    private bool isShrinking = false;

    /// <summary>Bool check if expanding input is being pressed</summary>
    private bool isExpanding = false;

    /// <summary>Bool check if rotating right input is being pressed</summary>
    private bool isRotatingR = false;

    /// <summary>Bool check if rotating left input is being pressed</summary>
    private bool isRotatingL = false;

    /// <summary>Bool check if mouse is performing rotation</summary>
    private bool isRotatingMouse = false;

    /// <summary>Bool check if mouse is performing shifting</summary>
    private bool isShiftingMouse = false;

    private float zoomFrame = 0f;

    private Vector2 shiftFrame = new Vector2();

    private float rotateFrame = 0;

    private Vector2 mouseDelta;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _cinematicBars = GetComponent<CinematicBarManager>();

        playerInput = FindObjectOfType<PlayerInput>();

        /// <summary>TODO</summary>
        shrinkAct = playerInput.actions["Shrink"];
        /// <summary>TODO</summary>
        expandAct = playerInput.actions["Expand"];
        /// <summary>TODO</summary>
        rotate = playerInput.actions["Rotate"];
        /// <summary>TODO</summary>
        rotateRAct = playerInput.actions["RotateRight"];
        /// <summary>TODO</summary>
        rotateLAct = playerInput.actions["RotateLeft"];
        /// <summary>TODO</summary>
        shiftCamXY = playerInput.actions["ShiftCamXY"];
        /// <summary>TODO</summary>
        shiftCamXYMouse = playerInput.actions["ShiftCamXYMouse"];
        /// <summary>TODO</summary>
        mouseMovement = playerInput.actions["MouseMovement"];

        /// <summary>Link the callbacks to shrinking state bools</summary>
        shrinkAct.performed += _ => isShrinking = true;
        shrinkAct.canceled += _ => isShrinking = false;

        /// <summary>Link the callbacks to expanding state bools</summary>
        expandAct.performed += _ => isExpanding = true;
        expandAct.canceled += _ => isExpanding = false;

        /// <summary>Link the callbacks to rotating right bools</summary>
        rotateRAct.performed += _ => isRotatingR = true;
        rotateRAct.canceled += _ => isRotatingR = false;

        /// <summary>Link the callbacks to rotating left bools</summary>
        rotateLAct.performed += _ => isRotatingL = true;
        rotateLAct.canceled += _ => isRotatingL = false;


        rotate.performed += _ => isRotatingMouse = true;
        rotate.canceled += _ => isRotatingMouse = false;

        /// <summary>Link the callbacks to shifting </summary>
        shiftCamXYMouse.performed += _ => isShiftingMouse = true;
        shiftCamXYMouse.canceled += _ => isShiftingMouse = false;

        mouseMovement.performed += _ => mouseDelta = _.ReadValue<Vector2>();
        mouseMovement.canceled += _ => mouseDelta = _.ReadValue<Vector2>();
    }

    /// <summary>CinematicBar Update Logic</summary>
    private void Update()
    {
        /// <summary>Reset Input readings per frame</summary>
        shiftFrame = Vector2.zero;
        rotateFrame = 0f;

        zoomFrame = playerInput.actions["MouseZoom"].ReadValue<Vector2>().normalized.y;

        /// <summary>When isShrinking is true then shrink the zoom frame</summary>
        if (isShrinking)
        {
            zoomFrame -= controllerZoomSpeed * Time.deltaTime;
        }

        /// <summary>When isExpanding is true then expand the zoom frame</summary>
        if (isExpanding)
        {
            zoomFrame += controllerZoomSpeed * Time.deltaTime;
        }

        /// <summary>When isRotatingR is true then rotate the zoom frame right</summary>
        if (isRotatingR)
        {
            rotateFrame -= controllerRotateSpeed * Time.deltaTime;
        }

        /// <summary>When isRotatingL is true then rotate the zoom frame Left</summary>
        if (isRotatingL) // Left
        {
            rotateFrame += controllerRotateSpeed * Time.deltaTime;
        }

        if (isRotatingMouse)
        {
            rotateFrame += GetCircularMotion();
        }

        /// <summary>If shiftCamXY returns a non zero vector then adjust the shiftFrame</summary>
        if (shiftCamXY.ReadValue<Vector2>() != Vector2.zero)
        {
            shiftFrame = shiftCamXY.ReadValue<Vector2>() * controllerMoveSpeed * Time.deltaTime;
        }
        /// <summary>If isShiftingMouse then adjust the shiftFrame by the inverse</summary>
        if (isShiftingMouse)
        {
            shiftFrame -= mouseDelta;
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
            _cinematicBars.offset -= (Vector2)Camera.main.ScreenToViewportPoint(shiftFrame * moveSpeed);
        }

        // move mouse right whilst holding left mouse button to rotate
        if (enableRotation && rotateFrame != 0)
        {
            _cinematicBars.rotation += rotateFrame * rotateSpeed;
        }
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
}
