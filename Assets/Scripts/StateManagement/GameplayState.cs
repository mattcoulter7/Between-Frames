using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayState : State
{
    public bool shouldPause = false;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Gameplay", this);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 1f;
        AudioListener.pause = false;
        shouldPause = false;
        EventDispatcher.Instance.Dispatch("OnPlay");
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

    public void OnPause()
    {
        shouldPause = true;
    }
}
