using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseState : State
{
    public string toggledAnimationVariable = "isOpen";
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Close", this);
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(toggledAnimationVariable,false);
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
