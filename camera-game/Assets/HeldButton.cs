using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HeldButton : MonoBehaviour
{
    public float currentValue = 0f;
    public float holdTime = 2f;
    public string inputBinding;
    public Action performed;
    public UnityEvent onHoldEnd;
    public UnityEvent onHoldBegin;
    public UnityEvent onHoldCancel;

    private HeldPlayerInput heldPlayerInput;
    private Coroutine heldTimer = null;
    private Coroutine valueCoroutine = null;
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
        
        if (value > 0)
        {
            if (heldTimer != null) return;
            heldTimer = StartCoroutine(HeldTimer());
            onHoldBegin.Invoke();
            StartLerpingValue();
        }
    }

    private void OnHoldEnd()
    {
        performed.Invoke();
        onHoldEnd.Invoke();

        StopLerpingValue();
    }

    private void OnHoldCancel()
    {
        StopCoroutine(heldTimer);
        heldTimer = null;

        onHoldCancel.Invoke();
        StopLerpingValue();
    }

    private void StartLerpingValue()
    {
        if (valueCoroutine != null) StopLerpingValue();
        valueCoroutine = StartCoroutine(LerpValue(0, 100, holdTime));
    }

    private void StopLerpingValue()
    {
        if (valueCoroutine != null)
        {
            StopCoroutine(valueCoroutine);
            valueCoroutine = null;
        }
        currentValue = 0;
    }

    private IEnumerator LerpValue(float min,float max,float timeToMove)
    {
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            currentValue = Mathf.Lerp(min, max, t);
            yield return null;
        }
        currentValue = 0f;
    }
}
