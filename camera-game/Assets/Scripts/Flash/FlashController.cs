using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FlashController : MonoBehaviour
{
    public Flash flash;
    public float timeBetweenFlash = 5f;
    public UnityEvent onFlash;
    private PlayerInput playerInput;
    private InputAction FlashAct;


    private Coroutine _instance = null;
    
    void Awake()
    {
        if (playerInput == null)
        {
            playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        }

        FlashAct = playerInput.actions["Flash"];
    }
    // Update is called once per frame
    void Update()
    {
        //if (_instance == null && Input.GetButtonDown("Flash") || _instance == null && FlashAct.triggered)
        //{
        //    //Start the coroutine we define below named ExampleCoroutine.
        //    _instance = StartCoroutine(DoFlash());
        //}

        if (_instance == null && FlashAct.triggered)
        {
            //Start the coroutine we define below named ExampleCoroutine.
            _instance = StartCoroutine(DoFlash());
        }
    }

    IEnumerator DoFlash()
    {
        flash.doCameraFlash = true;
        onFlash.Invoke();

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeBetweenFlash);

        StopCoroutine(_instance);
        _instance = null;
    }
}
