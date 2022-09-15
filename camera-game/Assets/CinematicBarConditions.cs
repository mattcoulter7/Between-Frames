using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class ensures that a set of conditions is always true
/// Making transforms to CinematicBarManager has the potential to desatisfy a condition
/// If this happens, the values are set to the previous frames values
/// </summary>
public class CinematicBarConditions : MonoBehaviour
{
    public List<DynamicModifier> conditions;

    public float lastRotation;
    public Vector2 lastOffset;
    public float lastDistance;

    public int maxNumSolverIterations = 5;
    public float solverDepenetrationVelocityScalar = 1.5f;

    private CinematicBarManager cinematicBarManager;

    private void Awake()
    {
        cinematicBarManager = GetComponent<CinematicBarManager>();
        foreach (DynamicModifier condition in conditions)
        {
            condition.OnInitialise();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CaptureValues();
    }

    // LateUpdate is called after all update executions
    private void LateUpdate()
    {
        if (!ConditionsSatisfied())
        {
            SatisfyConditions();
            //ReturnToPreviousState();
        }
    }
    private void CaptureValues()
    {
        lastRotation = cinematicBarManager._rotation;
        lastOffset = cinematicBarManager._offset;
        lastDistance = cinematicBarManager._distance;
    }

    private void ReturnToPreviousState()
    {
        cinematicBarManager._rotation = lastRotation;
        cinematicBarManager._offset = lastOffset;
        cinematicBarManager._distance = lastDistance;
    }

    private void SatisfyConditions()
    {
        float rotationDiff = cinematicBarManager._rotation - lastRotation;
        Vector2 offsetDiff = cinematicBarManager._offset - lastOffset;
        float distanceDiff = cinematicBarManager._distance - lastDistance;

        for (int i = 0; !ConditionsSatisfied() && i < maxNumSolverIterations; i++)
        {
            cinematicBarManager._rotation -= rotationDiff * solverDepenetrationVelocityScalar;
            cinematicBarManager._offset -= offsetDiff * solverDepenetrationVelocityScalar;
            cinematicBarManager._distance -= distanceDiff * solverDepenetrationVelocityScalar;
        }
    }

    private bool ConditionsSatisfied()
    {
        foreach (DynamicModifier condition in conditions)
        {
            if ((bool)condition.GetValue())
            {
                return false;
            }
        }
        return true;
    }
}
