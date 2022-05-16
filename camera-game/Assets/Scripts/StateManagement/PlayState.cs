using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayState : State
{
    public string playSceneName;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Play", this);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 1f;
        SceneManager.LoadScene(playSceneName);
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
