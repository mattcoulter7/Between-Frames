using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class State : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    protected StateMachine stateMachine;
    protected Animator animator;
    protected virtual void Awake(){
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
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