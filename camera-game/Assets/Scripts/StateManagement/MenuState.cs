using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    bool shouldExit = false;
    public GameObject menuUI;
    public string menuName;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState(menuName, this);
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        menuUI.SetActive(true);
        
    }
    public override void Exit()
    {
        base.Exit();
        menuUI.SetActive(false);
    }

    public override void HandleInput()
    {
        shouldExit = Input.GetButtonDown("Cancel");
    }
    public override void HandleShouldChangeState()
    {
        if (shouldExit){
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