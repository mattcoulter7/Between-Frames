using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCloseState : State
{
    public string closedAnimationVariable = "isClosed";
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("GateClosed", this);
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(closedAnimationVariable,true);
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
