using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseState : State
{
    public EventSystem eventSystem;
    bool shouldUnpause = false;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    private PlayerInput playerInput;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Pause", this);
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        EventDispatcher.Instance.Dispatch("OnPause");
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("DefaultSelected"));
        eventSystem.firstSelectedGameObject = GameObject.FindGameObjectWithTag("DefaultSelected");
        playerInput.SwitchCurrentActionMap("UI");
    }
    public override void Exit()
    {
        base.Exit();
        pauseMenuUI.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
    }

    public void Update()
    {

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
    public void OnOptions()
    {
        stateMachine.AddState("Option");
    }
}