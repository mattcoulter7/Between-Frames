using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Linq;
using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;
using System.Collections;

public class InputDeviceChangeHandler : MonoBehaviour
{
    public static string currentDeviceType { get; private set; } = "Keyboard&Mouse";
    public float checkInterval = 2f;
    private PlayerInput playerInput;
    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput == null)
        {
            StartCoroutine(CheckForDeviceChange());
        }
    }
    private void Update()
    {
        if (playerInput.currentControlScheme != currentDeviceType)
        {
            HandleDeviceChange(playerInput.currentControlScheme);
        }
    }
    private IEnumerator CheckForDeviceChange()
    {
        while (true)
        {
            if (currentDeviceType == "Keyboard&Mouse")
            {
                if (AnyGamepadInput())
                {
                    HandleDeviceChange("Gamepad");
                    yield return new WaitForSeconds(checkInterval);
                }
            }
            else if (currentDeviceType == "Gamepad")
            {
                if (AnyKeyboardInput() || AnyMouseInput())
                {
                    HandleDeviceChange("Keyboard&Mouse");
                    yield return new WaitForSeconds(checkInterval);
                }
            }
            yield return new WaitForSeconds(checkInterval);
        }
    }

    private bool AnyMouseInput()
    {
        int i = 0;
        return Mouse.current.allControls.Any(x =>
        {
            bool result = (i != 0 && i != 13 && i != 14) && (x.IsPressed() || x.IsActuated()) && !x.synthetic;
            i++;
            return result;
        });
    }

    private bool AnyKeyboardInput()
    {
        return Keyboard.current.anyKey.isPressed;
    }

    private bool AnyGamepadInput()
    {
        if (Gamepad.current == null) return false;
        return Gamepad.current.allControls.Any(x => (x.IsPressed() || x.IsActuated()) && !x.synthetic);
    }

    private void HandleDeviceChange(string newDeviceType)
    {
        EventDispatcher.Instance.Dispatch("OnInputDeviceChange", newDeviceType);
        currentDeviceType = newDeviceType;
    }
}