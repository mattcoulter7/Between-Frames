using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedState : State
{
    public string stateName;
    public string animationBool;

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
        if (animator != null) animator.SetBool(animationBool,true);
    }
    public override void Exit()
    {
        base.Exit();
        if (animator != null) animator.SetBool(animationBool,false);
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
