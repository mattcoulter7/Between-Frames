using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurableState : State
{
    public string stateName;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState(stateName, this);
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Enter()
    {
        base.Enter();
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
