using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        shouldPause = false;
        EventDispatcher.Instance.Dispatch("OnPlay");
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OnPause();
        }
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
