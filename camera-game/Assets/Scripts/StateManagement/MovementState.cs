using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;


public class MovementState : State
{
    public float moveSpeed;
    public float jumpHeight;
    public float groundDistance = 0.2f;
    public LayerMask ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _jumpInput = false;
    public bool _isGrounded = false;
    //Testing
    public bool _isGroundedLastFrame = false;
    public float timeBetweenLandSFX = 0.5f;
    private Coroutine _instance = null;

    public string walkingAnimationVariable = "isWalking";
    public string jumpingAnimationVariable = "isJumping";
    public string fallingAnimationVariable = "isFalling";
    private Transform _groundChecker;

    public UnityEvent onJump;
    public UnityEvent onLand;
    public UnityEvent onMove;

    // David - new input system

    private PlayerInput playerInput;

    private InputAction jumpAct;

    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Movement", this);

        playerInput = GetComponent<PlayerInput>();
        //playerInput = new ControlInput();
        jumpAct = playerInput.actions["Jump"];
        //jumpAct = playerInput.Player.Jump;
        playerInput.actions["Move"].performed += ctx => Move();
    }

    protected override void Start()
    {
        base.Start();

        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }
    public override void HandleInput()
    {
        _inputs = Vector3.zero;
        //_inputs.x = Input.GetAxis("Horizontal");
        _inputs.x = playerInput.actions["Move"].ReadValue<Vector2>().x;

        //_jumpInput = Input.GetButtonDown("Jump");
        _jumpInput = jumpAct.triggered;
    }
    public override void HandleShouldChangeState()
    {
        if (_isGrounded && _inputs == Vector3.zero)
        {
            stateMachine.ChangeState("Standing");
            if (animator != null)
            {
                animator.SetBool(walkingAnimationVariable, false);
            }
        }
    }
    public override void LogicUpdate()
    {
  

        // look at downwards raycast for grounding
        _isGrounded = Physics.CheckSphere(_groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        //Testing
        if (_isGrounded && !_isGroundedLastFrame && _instance == null)
        {
            _instance = StartCoroutine(DoLand());
        }
        _isGroundedLastFrame = _isGrounded;
        ////////////////////////////////////
        if (_isGrounded) // on ground
        {
            animator.SetBool(fallingAnimationVariable, false);
            animator.SetBool(jumpingAnimationVariable, false);
            if (_inputs != Vector3.zero) // is walking
            {
                transform.forward = _inputs;
                if (animator != null)
                {
                    animator.SetBool(walkingAnimationVariable, true);
                }
            }
            else
            { // is not walking
                animator.SetBool(walkingAnimationVariable, false);
            }

            if (_jumpInput) // if jumping
            {
                onJump.Invoke();
                _body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                _isGrounded = false;
                if (animator != null)
                {
                    animator.SetBool(jumpingAnimationVariable, true);
                }
            }
        }
        else // in air
        {
            animator.SetBool(walkingAnimationVariable, false);
            if (_body.velocity.y < 0)
            {
                if (animator != null)
                {
                    animator.SetBool(jumpingAnimationVariable, false);
                    animator.SetBool(fallingAnimationVariable, true);
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        _body.MovePosition(_body.position + _inputs * moveSpeed * Time.fixedDeltaTime);
    }

    IEnumerator DoLand()
    {

        onLand.Invoke();

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeBetweenLandSFX);

        StopCoroutine(_instance);
        _instance = null;
    }

    //IEnumerator OnJump()
    //{
    //    onJump.Invoke();
    //    Debug.Log("A test");
    //    yield return null;
    //}

    private void OnJump(InputValue aValue)
    {
        if (_isGrounded)
        {
            if (_jumpInput) // if jumping
            {
                onJump.Invoke();
                _body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                _isGrounded = false;
                if (animator != null)
                {
                    animator.SetBool(jumpingAnimationVariable, true);
                }
            }
        }
    }

    private void OnMove(InputValue aValue)
    {
        onMove.Invoke();
    }

   public void Move()
    {
        //Debug.Log(playerInput.actions["Move"].ReadValue<Vector2>().x);
        //_inputs.x = playerInput.actions["Move"].ReadValue<Vector2>().x;
        //Debug.Log(playerInput.Player.Move.ReadValue<Vector2>().x);
        
        //_inputs.x = playerInput.Player.Move.ReadValue<Vector2>().x;
        _inputs.x = playerInput.actions["Move"].ReadValue<Vector2>().x;
        //_body.MovePosition(_body.position + _inputs * moveSpeed * Time.fixedDeltaTime);
        //Debug.Log(_inputs.x);

    }
   
    //private void OnEnable()
    //{
    //    playerInput.Enable();
    //}
    //
    //private void OnDisable()
    //{
    //    playerInput.Disable();
    //}

    //public ControlInput GetInput()
    //{
    //    return playerInput;
    //}
}
//if (_instance == null && Input.GetButtonDown("Flash"))
//{
//    //Start the coroutine we define below named ExampleCoroutine.
//    _instance = StartCoroutine(DoFlash());
//}