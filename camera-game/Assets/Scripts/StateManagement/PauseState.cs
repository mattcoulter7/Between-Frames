using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : State
{
    bool shouldUnpause = false;
    public GameObject pauseMenuUI;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Pause", this);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }
    public override void Exit()
    {
        base.Exit();
        pauseMenuUI.SetActive(false);
    }

    public override void HandleInput()
    {
        shouldUnpause = Input.GetButtonDown("Cancel");
    }
    public override void HandleShouldChangeState()
    {
        if (shouldUnpause){
            stateMachine.RemoveState();
        }
    }
    public override void LogicUpdate()
    {
    }
    public override void PhysicsUpdate()
    {
    }
}