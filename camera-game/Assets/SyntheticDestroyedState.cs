using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntheticDestroyedState : State
{
    public GameObject destroyedPrefab;
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;
    private GameObject destroyedInstance = null;
    public void OnDestroyed()
    {
        rigidBody.detectCollisions = false;
        meshRenderer.enabled = false;
        rigidBody.isKinematic = true;

        CreateDestroyedInstance();
    }
    public void OnRestored()
    {
        rigidBody.detectCollisions = true;
        meshRenderer.enabled = true;
        rigidBody.isKinematic = false;

        DestroyDestroyedInstance();
    }
    public void CreateDestroyedInstance()
    {
        destroyedInstance = Instantiate(destroyedPrefab, transform.position,transform.rotation);
    }

    public void DestroyDestroyedInstance()
    {
        Destroy(destroyedInstance);
    }

    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("SyntheticDestroyed", this);
        rigidBody = GetComponentInChildren<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }


    public override void Enter()
    {
        base.Enter();
        OnDestroyed();
    }
    public override void Exit()
    {
        base.Exit();
        OnRestored();
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