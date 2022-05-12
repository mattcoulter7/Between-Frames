using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
public class StandingState : State
{
    bool movementInput = false;
    bool jumpInput = false;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Standing", this);
    }

    public override void HandleInput()
    {
        movementInput = (Input.GetAxis("Horizontal") != 0f) || Input.GetButton("Jump");
        jumpInput = Input.GetButtonDown("Jump");
    }
    public override void HandleShouldChangeState()
    {
        if (movementInput || jumpInput)
        {
            stateMachine.ChangeState("Movement");
        }
    }
    public override void LogicUpdate()
    {
    }
    public override void PhysicsUpdate()
    {
    }
}