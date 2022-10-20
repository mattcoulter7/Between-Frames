using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HeldButton : MonoBehaviour
{
    public float holdTime = 2f;
    public string inputBinding;
    public Action performed;
    public UnityEvent onHoldEnd;
    public UnityEvent onHoldBegin;
    public UnityEvent onHoldCancel;

    private HeldPlayerInput heldPlayerInput;
    private Coroutine heldTimer = null;
    // Start is called before the first frame update
    void Awake()
    {
        heldPlayerInput = FindObjectOfType<HeldPlayerInput>();
        performed = new Action(() => { });
    }

    private void OnEnable()
    {
        heldPlayerInput.performed[inputBinding] += OnHoldBegin;
    }

    private IEnumerator HeldTimer()
    {
        yield return new WaitForSeconds(holdTime);
        OnHoldEnd();
    }

    private void OnHoldBegin(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        if (value == 0) OnHoldCancel();

        if (heldTimer != null) return;

        heldTimer = StartCoroutine(HeldTimer());
        onHoldBegin.Invoke();
    }

    private void OnHoldEnd()
    {
        performed.Invoke();
        onHoldEnd.Invoke();
    }

    private void OnHoldCancel()
    {
        StopCoroutine(heldTimer);
        heldTimer = null;
        onHoldCancel.Invoke();
    }
}
