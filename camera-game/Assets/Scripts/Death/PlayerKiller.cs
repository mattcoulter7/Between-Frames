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

    CapsuleCollider _myCol;
    bool ready = false;
    void Start()
    {
        _myCol = GetComponent<CapsuleCollider>();
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(ExampleCoroutine());
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
            //Debug.Log(distance);
            if (distance > condition.squishTolerance){
                kill = true;
            }
        }

        if (kill)
        {
            onKill.Invoke();
            Debug.Log("Killed");            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
