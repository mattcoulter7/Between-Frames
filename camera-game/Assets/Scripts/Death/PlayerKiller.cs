using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerKiller : MonoBehaviour
{
    [System.Serializable]
    public class KillCondition
    {
        public Collider col;
        public float squishTolerance = 10f;
    }
    public List<KillCondition> killConditions = new List<KillCondition>();
    public UnityEvent onKill;

    Collider _myCol;
    bool ready = false;

    public bool dead;
    void Start()
    {
        _myCol = GetComponent<Collider>();
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(ExampleCoroutine());
        dead = false;
    }
    public void Reset()
    {
        dead = false;
    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);
        ready = true;
    }

    void Update()
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
