using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
public class DeathState : State
{
    public string deathAnimationVariable = "isDead";
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Dead", this);
    }

    public override void Enter()
    {
        base.Enter();
        base.Enter();
        if (animator != null){
        animator.SetBool(deathAnimationVariable,true);
        }
    }

    public override void HandleInput()
    {
    }
    public override void HandleShouldChangeState()
    {
        //stateMachine.ChangeState("Rewind");
    }
    public override void LogicUpdate()
    {
    }
    public override void PhysicsUpdate()
    {
    }
}