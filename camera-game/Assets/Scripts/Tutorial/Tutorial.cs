using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class Tutorial : MonoBehaviour
{
    private PlayerInput playerInput;

    public void OnSubmit(InputAction.CallbackContext ctx)
    {
        playerInput.actions["Submit"].performed -= OnSubmit;
        playerInput.SwitchCurrentActionMap("Player");
        Destroy(gameObject);
    }
    private void OnEnable()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        playerInput.SwitchCurrentActionMap("UI");
        playerInput.actions["Submit"].performed += OnSubmit;
    }

    private void OnDisable()
    {
        playerInput.actions["Submit"].performed -= OnSubmit;
    }
}
