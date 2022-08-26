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
        // Register Each collider with a state to avoid checking collision penetration of colliders which aren't colliding
        foreach (Collider bound in bounds)
        {
            collisionStates.Add(bound, false);
        }
        if (myCollider == null)
        {
            myCollider = GetComponent<Collider>();
        }

    }
    public override void Update()
    {
        base.Update();

        foreach (Collider bound in bounds)
        {
            // Don't check penetration if the objects aren't touching, 0 is implied.
            if (!collisionStates[bound]) continue;

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
                OnTriggerBegin();
            } else {
                OnTriggerEnd();
            }
        }
    }

    public override void OnTriggerBegin()
    {
        if (_inBounds)
        {
            base.OnTriggerBegin();
        }
    }
    public override void OnTriggerEnd()
    {
        base.OnTriggerEnd();
    }

    private void OnTriggerEnter(Collider other)
    {
        collisionStates[other] = true;
    }

    private void OnTriggerStay(Collider other)
    {
        collisionStates[other] = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collisionStates[other] = false;
    }
}
