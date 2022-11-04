using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputActionBinding : MonoBehaviour
{
    public string inputAction;
    public UnityEvent callback;
    private PlayerInput playerInput;
    private void OnEnable()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            playerInput.actions[inputAction].performed += Callback;
    }

    private void OnDisable()
    {
        if (playerInput != null)
            playerInput.actions[inputAction].performed -= Callback;
    }
    private void OnDestroy()
    {
        if (playerInput != null)
            playerInput.actions[inputAction].performed -= Callback;
    }

    public void Callback(InputAction.CallbackContext ctx)
    {
        if (this == null) return;
        callback.Invoke();
    }
}
