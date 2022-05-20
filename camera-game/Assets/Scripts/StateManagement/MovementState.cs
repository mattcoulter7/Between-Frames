using UnityEngine;
using UnityEngine.Events;
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
    public string walkingAnimationVariable = "isWalking";
    public string jumpingAnimationVariable = "isJumping";
    public string fallingAnimationVariable = "isFalling";
    private Transform _groundChecker;

    public UnityEvent whileWalking;
    public UnityEvent onJump;

    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Movement", this);
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
        _inputs.x = Input.GetAxis("Horizontal");
        _jumpInput = Input.GetButtonDown("Jump");
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
                    whileWalking.Invoke();
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
}