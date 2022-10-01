using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Linq;
using UnityEngine.InputSystem.Controls;

public class InputDeviceChangeHandler : MonoBehaviour
{
    public static string currentDeviceType { get; private set; } = "MouseKeyboard";
    private Vector3 lastMouseCoordinate = Vector3.zero;
    private void Update()
    {
        if (currentDeviceType == "MouseKeyboard")
        {
            if (AnyGamepadInput())
            {
                HandleDeviceChange("Gamepad");
                return;
            }
        }
        else if (currentDeviceType == "Gamepad")
        {
            if (AnyKeyboardInput() || AnyMouseInput())
            {
                HandleDeviceChange("MouseKeyboard");
                return;
            }
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