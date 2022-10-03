using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class Tutorial : MonoBehaviour
{
    PlayerInput myInput;
    bool bDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        myInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
    }

    void Awake()
    {

    }

    private void OnEnable()
    {
        myInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        myInput.SwitchCurrentActionMap("UI");
        myInput.actions["Submit"].performed += OnSubmit;
    }

    private void OnDisable()
    {
        myInput.actions["Submit"].performed -= OnSubmit;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        try
        {
            if (bDestroyed != true)
            {
                if (myInput == null)
                {
                    myInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
                }
                myInput.SwitchCurrentActionMap("Player");

                if (bDestroyed)
                {
                    return;
                }
                else if (this != null)
                {
                    bDestroyed = true;
                    gameObject.SetActive(false);
                    //Destroy(this);

                    //if (gameObject.transform.GetChild(0).gameObject != null)
                    //{
                    //    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    //
                    //}
                }

            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
}
