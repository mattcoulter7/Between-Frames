using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBounds : MonoBehaviour
{
    public LayerMask restrict;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!restrict.HasLayer(other.gameObject.layer)) return;

        StateMachine stateMachine = other.GetComponent<StateMachine>();
        stateMachine.AddState("Dead");
    }
}
