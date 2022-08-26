using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : Trigger
{
    public string actionString;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (Input.GetAxis(actionString) != 0){

            OnTriggerBegin();

        }

         
    }
}
