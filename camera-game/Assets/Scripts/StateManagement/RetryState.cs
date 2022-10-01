using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryState : State
{
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Retry", this);
    }

    public override void Enter()
    {
        base.Enter();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
