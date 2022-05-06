using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
public class DeathState : State
{
    public override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Dead", this);
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool("isDead",true);
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