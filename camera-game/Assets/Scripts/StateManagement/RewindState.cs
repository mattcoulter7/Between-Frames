using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class RewindState : State
{
    Rigidbody _rb;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Rewind", this);
    }
    protected override void Start()
    {
        base.Start();
        EventDispatcher.AddEventListener("OnRewindFinish", (Action<Rewinder>)OnRewindFinish);
        _rb = GetComponent<Rigidbody>();
    }

    public override void Enter()
    {
        base.Enter();
        _rb.isKinematic = false;
        _rb.detectCollisions = true;
        _rb.useGravity = false;
        Rewinder.Instance.BeginRewind();
    }
    public override void Exit()
    {
        base.Exit();
        _rb.isKinematic = true;
        _rb.detectCollisions = false;
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

    void OnRewindFinish(Rewinder rewinder)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}