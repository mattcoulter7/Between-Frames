using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeState : State
{
    public float rechargeTime = 5f;
    private BlendShapeBlender _blender;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.RegisterState("Recharge", this);
    }
    protected override void Start()
    {
        base.Start();
        _blender = GetComponent<BlendShapeBlender>();
    }
    public override void Enter()
    {
        base.Enter();
        _blender.value = 0;
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
        float t = 0;
        while (t < 1)
        {
            _blender.value = Mathf.Lerp(0, 100, t);
            t += Time.deltaTime / rechargeTime;
            yield return null; 
        }
        _blender.value = 100;
        stateMachine.RemoveState();
    }
}
