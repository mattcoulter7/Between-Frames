using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsTrigger : Trigger
{
    public List<Collider> bounds;
    public Collider myCollider;
    public float penetrationDepth = 0.5f;
    public bool automaticInvokation = false;
    private bool _inBounds;
    private Dictionary<Collider, bool> collisionStates = new Dictionary<Collider, bool>();
    void Start()
    {
        if (myCollider == null) myCollider = GetComponent<Collider>();
    }
    public override void Update()
    {
        base.Update();

        foreach (Collider bound in bounds)
        {
            Vector3 direction;
            float distance;
            Physics.ComputePenetration(
                bound,
                bound.transform.position,
                bound.transform.rotation,
                myCollider,
                myCollider.transform.position,
                myCollider.transform.rotation,
                out direction,
                out distance
            );
            if (distance > penetrationDepth)
            {
                SetInBounds(true);
                return;
            }
        }
        SetInBounds(false);
    }

    public void SetInBounds(bool inBounds)
    {
        bool shouldTrigger = automaticInvokation && _inBounds != inBounds;
        _inBounds = inBounds;

        if (shouldTrigger) // values is changing
        {
            if (_inBounds){
                OnTriggerStart();
            } else {
                OnTriggerEnd();
            }
        }
    }

    public override void OnTriggerStart()
    {
        if (_inBounds)
        {
            base.OnTriggerStart();
        }
    }
    public override void OnTriggerEnd()
    {
        base.OnTriggerEnd();
    }
}
