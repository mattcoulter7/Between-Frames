using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUIBinder : MonoBehaviour
{
    public string inputBinding;
    private InteractionUIBindings interactionUIBindings;
    private Image icon;
    // Start is called before the first frame update
    void Start()
    {
        interactionUIBindings = FindObjectOfType<InteractionUIBindings>();
        icon = GetComponent<Image>();
        SetIcon(InputDeviceChangeHandler.currentDeviceType);
        EventDispatcher.Instance.AddEventListener("OnInputDeviceChange", (Action<string>)SetIcon);
    }
    private void SetIcon(string deviceType)
    {
        if (this != null)
        {
            // set the sprite
            Sprite boundSprite = interactionUIBindings.GetSprite(inputBinding, deviceType);
            icon.sprite = boundSprite;
        }
    }
}
