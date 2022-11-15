using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuState : State
{
    bool shouldExit = false;
    public GameObject menuUI;
    public string menuName;
    public bool stopTime = true;
    public bool canExit = true;
    private PlayerInput playerInput;
    private EventSystem eventSystem;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState(menuName, this);
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = stopTime ? 0f : 1f;
        menuUI.SetActive(true);

        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("DefaultSelected"));
        eventSystem.firstSelectedGameObject = GameObject.FindGameObjectWithTag("DefaultSelected");

        playerInput.SwitchCurrentActionMap("UI");
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
        if (canExit && shouldExit)
        {
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