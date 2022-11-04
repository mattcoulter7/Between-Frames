using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : State
{
    public GameObject mainMenuUI;
    public string mainMenuSceneName = "MenuTest";
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("MainMenu", this);
    }

    public override void Enter()
    {
        base.Enter();
        if (SceneManager.GetActiveScene().name != mainMenuSceneName)
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        if (mainMenuUI != null){
            mainMenuUI.SetActive(true);
        }
    }
    public override void Exit()
    {
        base.Exit();
        if (mainMenuUI != null){
            mainMenuUI.SetActive(false);
        }
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
