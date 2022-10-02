using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeState : State
{
    public float rechargeTime = 5f;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Recharge", this);
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Process());
    }
    public override void Exit()
    {
        base.Exit();
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

    IEnumerator Process()
    {
        yield return new WaitForSeconds(rechargeTime);
        stateMachine.RemoveState();
    }
}
