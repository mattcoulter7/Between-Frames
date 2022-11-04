using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Linq;
using System;

public class HeldPlayerInput : MonoBehaviour
{
    /// <summary>
    /// Holds the mappings from action name to callbacks.
    /// </summary>
    public Dictionary<string, System.Action<InputAction.CallbackContext>> performed { get; private set; } = new Dictionary<string, System.Action<InputAction.CallbackContext>>();

    /// <summary>
    /// The coroutines which run the callback every frame whilst the key is being held down
    /// </summary>
    private Dictionary<InputAction, Coroutine> actionCoroutines = new Dictionary<InputAction, Coroutine>();

    /// <summary>
    /// Reference the all the input actions that are configured with a hold and a press interaction
    /// </summary>
    private List<InputAction> inputActions = new List<InputAction>();

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        GetHoldInputActions();
        InitialiseActions();
    }
    private void OnEnable()
    {
        BindHoldActions();
    }

    private void OnDisable()
    {
        UnbindHoldActions();
    }

    /// <summary>
    /// Get all of the (cache) ActionInputs that are configured properly in order for the continuous hold callback to work
    /// </summary>
    private List<InputAction> GetHoldInputActions()
    {
        inputActions = playerInput.actions.Where(ia => ia.interactions.Contains("Hold") && ia.interactions.Contains("Press")).ToList();
        return inputActions;
    }

    /// <summary>
    /// Set up a new System.Action callback for each of the actions.
    /// This way we can bind and unbind various functions.
    /// </summary>
    private void InitialiseActions()
    {
        foreach (InputAction ia in inputActions)
        {
            performed[ia.name] = new System.Action<InputAction.CallbackContext>((ctx) => { });
        }
    }

    /// <summary>
    /// Bind OnHold to listen to when the Hold Input is triggered
    /// </summary>
    private void BindHoldActions()
    {
        foreach (InputAction ia in inputActions)
        {
            ia.performed += OnHold;
        }
    }

    /// <summary>
    /// Bind OnHold from listening to when the Hold Input is triggered
    /// </summary>
    private void UnbindHoldActions()
    {
        foreach (InputAction ia in inputActions)
        {
            ia.performed -= OnHold;
        }
    }

    /// <summary>
    /// Handle Starting and Stopping the OnHoldCallback Coroutines for each InputAction.
    /// </summary>
    private void OnHold(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        if (ctx.interaction is HoldInteraction)
        {
            actionCoroutines[ctx.action] = StartCoroutine(OnHoldCallback(ctx));
        }
        else if (ctx.interaction is PressInteraction)
        {
            if (value == 0)
            {
                Coroutine heldCoroutine = null;
                actionCoroutines.TryGetValue(ctx.action, out heldCoroutine);
                if (heldCoroutine != null)
                    StopCoroutine(heldCoroutine);
            }
        }
        //Debug.Log(value);
        //Debug.Log(ctx.interaction);
    }

    /// <summary>
    /// Invoke the Hold Callbacks continuously (every frame)
    /// </summary>
    private IEnumerator OnHoldCallback(InputAction.CallbackContext ctx)
    {
        while (true)
        {
            try
            {
                //Debug.Log(ctx.ReadValue<float>());
                performed[ctx.action.name].Invoke(ctx);
            }
            catch (Exception e)
            {
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
