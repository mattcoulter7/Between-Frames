using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCursorHIder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.AddEventListener("OnInputDeviceChange", (Action<string>)OnInputDeviceChange);
    }

    private void ToggleMouseVisibility(bool show)
    {
        Cursor.visible = show;
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnInputDeviceChange(string inputDevice)
    {
        if (inputDevice == "Keyboard&Mouse")
        {
            ToggleMouseVisibility(true);
        }
        else
        {
            ToggleMouseVisibility(false);
        }
    }
}
