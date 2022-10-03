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
        playerInput.actions[inputAction].performed += Callback;
    }

    private void OnDisable()
    {
        playerInput.actions[inputAction].performed -= Callback;
    }
    private void OnDestroy()
    {
        playerInput.actions[inputAction].performed -= Callback;
    }

    public void Callback(InputAction.CallbackContext ctx)
    {
        if (this == null) return;
        callback.Invoke();
    }
}
