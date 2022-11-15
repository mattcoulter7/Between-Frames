using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class HeldButton : MonoBehaviour
{
    public float holdTime = 2f;
    public string inputBinding;
    public Animator buttonAnimator;
    public UnityEvent onHoldEnd;
    public UnityEvent onHoldBegin;
    public UnityEvent onHoldCancel;

    private PlayerInput playerInput;
    private Coroutine heldTimer = null;
    private Coroutine valueCoroutine = null;
    private float currentValue = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void OnEnable()
    {
        //heldPlayerInput.performed[inputBinding] += OnHoldBegin;
        playerInput.actions[inputBinding].performed += OnHoldBegin;
        playerInput.actions[inputBinding].canceled += OnHoldCancel;
    }

    private void OnDisable()
    {
        //heldPlayerInput.performed[inputBinding] -= OnHoldBegin;
        playerInput.actions[inputBinding].performed -= OnHoldBegin;
        playerInput.actions[inputBinding].canceled -= OnHoldCancel;
    }

    private void Update()
    {
        buttonAnimator.SetFloat("Progress", currentValue);
    }

    private IEnumerator HeldTimer()
    {
        yield return new WaitForSeconds(holdTime);
        OnHoldEnd();
    }

    private void OnHoldBegin(InputAction.CallbackContext ctx)
    {
        if (heldTimer != null) return;
        heldTimer = StartCoroutine(HeldTimer());
        onHoldBegin.Invoke();
        StartLerpingValue();
    }

    private void OnHoldCancel(InputAction.CallbackContext ctx)
    {
        if (heldTimer != null)
        {
            StopCoroutine(heldTimer);
            heldTimer = null;
        }

        onHoldCancel.Invoke();
        StopLerpingValue();
    }
    private void OnHoldEnd()
    {
        onHoldEnd.Invoke();

        StopLerpingValue();
    }

    private void StartLerpingValue()
    {
        if (valueCoroutine != null) StopLerpingValue();
        valueCoroutine = StartCoroutine(LerpValue(0, 1, holdTime));
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
