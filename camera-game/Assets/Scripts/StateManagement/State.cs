using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary></summary>
public abstract class State : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public string layer;
    protected StateMachine stateMachine;
    protected Animator animator;
    protected virtual void Awake(){
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        stateMachine.SelectLayer(layer);
    }
    protected virtual void Start(){
    }
    public virtual void Enter(){
        OnEnter.Invoke();
        // needs to process input and change state because state may change mid frame!
        HandleInput();
    }

    public virtual void Exit(){
        OnExit.Invoke();
    }
    public abstract void HandleInput();

    public abstract void HandleShouldChangeState();
    public abstract void LogicUpdate(); // Invoked by StateMachine

    public abstract void PhysicsUpdate(); // Invoked by StateMachine
}