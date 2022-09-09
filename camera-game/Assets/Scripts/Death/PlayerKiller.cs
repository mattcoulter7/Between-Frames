using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

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
    private List<Collider> killColliders;

    /// <summary>Unity Event for what else will happen on kill such as a death sound playing</summary>
    public UnityEvent onKill;

    /// <summary>True if the player is dead</summary>
    public bool dead;

    private Dictionary<Collider, bool> collisionStates = new Dictionary<Collider, bool>();

    private Collider _myCol;

    /// <summary>Sets dead back to false</summary>
    public void Reset()
    {
        dead = false;
    }

    private void Start()
    {
        _myCol = GetComponent<Collider>();
        //Start the coroutine we define below named ExampleCoroutine.
        dead = false;
        killColliders = killConditions.Select(kc => kc.col).ToList();
    }

    private void FixedUpdate()
    {
        // No need to check for death if already dead
        if (dead) return;

        bool kill = false;

        // need to have at least 2 colliders for a squish to happen
        int collisionCount = collisionStates.Values.Where(col => col == true).ToList().Count;
        if (collisionCount < 2) return;

        foreach (KillCondition condition in killConditions)
        {
            if (condition.col == null) continue; // no collider configured, ignore null error

            bool currentlyColliding = false;
            collisionStates.TryGetValue(condition.col, out currentlyColliding);
            if (!currentlyColliding) continue; // not currently colliding with the collider, don't care

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
            if (distance > condition.squishTolerance)
            {
                dead = true;
                onKill.Invoke();
                return;
            }
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (!killColliders.Contains(col.collider)) return; // only track colliders which can kill the object
        collisionStates[col.collider] = true;
    }

    private void OnCollisionStay(Collision col)
    {
        if (!killColliders.Contains(col.collider)) return; // only track colliders which can kill the object
        collisionStates[col.collider] = true;
    }

    private void OnCollisionExit(Collision col)
    {
        if (!killColliders.Contains(col.collider)) return; // only track colliders which can kill the object
        collisionStates[col.collider] = false;
    }
}
