using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class Tutorial : MonoBehaviour
{
    private PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        playerInput.SwitchCurrentActionMap("UI");
        playerInput.actions["Submit"].performed += OnSubmit;
    }

    private void OnDisable()
    {
        playerInput.actions["Submit"].performed -= OnSubmit;
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        try
        {
                if (playerInput == null)
                {
                    playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
                }
                playerInput.SwitchCurrentActionMap("Player");

                if (this != null)
                {
                    Destroy(gameObject);
                }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
}
