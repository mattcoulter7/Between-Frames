using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenState : State
{
    public string openAnimationVariable = "isOpen";
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("GateOpen", this);
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(openAnimationVariable,true);
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