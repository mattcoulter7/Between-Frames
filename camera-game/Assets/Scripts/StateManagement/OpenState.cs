using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenState : State
{
    public string toggledAnimationVariable = "isOpen";
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Open", this);
    }

    public override void Enter()
    {
        base.Enter();
        if (animator != null)
        {
            animator.SetBool(toggledAnimationVariable, true);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
    }
    public override void HandleShouldChangeState()
    {
    }
    public override void LogicUpdate()
    {
    }
    public override void PhysicsUpdate()
    {
    }
}