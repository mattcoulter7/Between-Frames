using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
public class DeathState : State
{
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Dead", this);
        EventDispatcher.Instance.AddEventListener("OnRewindContinue",(Action)OnRewindContinue);
    }

    public override void Enter()
    {
        base.Enter();
        Rewinder2 rewinder = FindObjectOfType<Rewinder2>();
        rewinder.RewindForSeconds();
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

    private void OnRewindContinue()
    {
        if (stateMachine.currentState == this)
            stateMachine.RemoveState();
    }
}