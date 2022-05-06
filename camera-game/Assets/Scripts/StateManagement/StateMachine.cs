using UnityEngine;
using System;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
    public string defaultState;
    public State currentState;
    Dictionary<string, State> states = new Dictionary<string, State>();
    void Start()
    {
        ChangeState(defaultState);
    }

    void Update()
    {
        currentState.HandleInput();
        currentState.HandleShouldChangeState();
        currentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    public void ChangeState(string state)
    {
        if (currentState != null) currentState.Exit();
        currentState = states[state];
        currentState.Enter();
    }

    public void RegisterState(string value, State obj)
    {
        states[value] = obj;
    }
}