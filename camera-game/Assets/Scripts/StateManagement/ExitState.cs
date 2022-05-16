using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitState : State
{
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Exit", this);
    }

    public override void Enter()
    {
        base.Enter();
        Application.Quit();
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
