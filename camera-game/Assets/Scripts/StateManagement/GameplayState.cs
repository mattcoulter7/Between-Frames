using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : State
{
    bool shouldPause = false;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Gameplay", this);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 1f;
        EventDispatcher.Instance.Dispatch("OnPlay");
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        shouldPause = Input.GetButtonDown("Cancel");
    }
    public override void HandleShouldChangeState()
    {
        if (shouldPause){
            stateMachine.AddState("Pause");
        }
    }
    public override void LogicUpdate()
    {
    }
    public override void PhysicsUpdate()
    {
    }
}
