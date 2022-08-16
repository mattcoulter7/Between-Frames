using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles determining if a player is killed by configuring kill conditions
/// </summary>
public class PlayerKiller : MonoBehaviour
{
    [System.Serializable]
    /// <summary>Determines how far an object needs to be within a collider for a kill to occur</summary>
    public class KillCondition
    {
        public Collider col;
        public float squishTolerance = 10f;
    }

    /// <summary>The list of kill conditions which will trigger a kill if any one of them are true</summary>
    public List<KillCondition> killConditions = new List<KillCondition>();

    /// <summary>Unity Event for what else will happen on kill such as a death sound playing</summary>
    public UnityEvent onKill;

    /// <summary>True if the player is dead</summary>
    public bool dead;

    private CapsuleCollider _myCol;
    private bool ready = false;

    /// <summary>Sets dead back to false</summary>
    public void Reset()
    {
        dead = false;
    }

    private void Start()
    {
        _myCol = GetComponent<CapsuleCollider>();
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(ExampleCoroutine());
        dead = false;
    }

    private IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);
        ready = true;
    }

    private void Update()
    {
        if (!ready) return;
        bool kill = false;

        foreach (KillCondition condition in killConditions)
        {
            Vector3 direction;
            float distance;
            Physics.ComputePenetration(
                condition.col,
                condition.col.transform.position,
                condition.col.transform.rotation,
                _myCol,
                _myCol.transform.position,
                _myCol.transform.rotation,
                out direction,
                out distance
            );
            //Debug.Log(distance);
            if (distance > condition.squishTolerance){
                kill = true;
            }
        }

        if (kill && !dead)
        {
            dead = true;
            onKill.Invoke();
        }
    }
}
