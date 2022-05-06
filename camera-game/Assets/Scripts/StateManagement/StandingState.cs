using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
public class StandingState : State
{
    bool movementInput = false;
    public override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Standing", this);
    }

    public override void HandleInput()
    {
        movementInput = (Input.GetAxis("Horizontal") != 0f) || Input.GetButton("Jump");
    }
    public override void HandleShouldChangeState()
    {
        if (movementInput)
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