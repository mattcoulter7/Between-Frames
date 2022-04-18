using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent onDoorEnter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterDoor(){
        onDoorEnter.Invoke();
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("player")){
            EnterDoor();
        }
    }
}
