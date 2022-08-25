using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
public class StandingState : State
{
    bool movementInput = false;
    bool controllerInput = false;
    bool jumpInput = false;
    bool controllerJumpInput;
    private PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Standing", this);
        
        if (playerInput == null)
        {
            playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        }
    }

    public override void HandleInput()
    {
        movementInput = (Input.GetAxis("Horizontal") != 0f) || Input.GetButton("Jump");
        
        if(playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero)
        {
            controllerInput = true;
        }
        
        else
        {
            controllerInput = false;
        }

        jumpInput = Input.GetButtonDown("Jump");
        controllerJumpInput = playerInput.actions["Jump"].triggered;
    }
    public override void HandleShouldChangeState()
    {
        if (movementInput || jumpInput || controllerInput || controllerJumpInput)
        {
            stateMachine.ChangeState("Movement");
        }
    }
    public override void LogicUpdate()
    {
        //Debug.Log(controllerInput);
    }
    public override void PhysicsUpdate()
    {
    }
}