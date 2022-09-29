using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// helper functions to easily set children based on interaction object
/// </summary>
public class InteractionMenuLine : MonoBehaviour
{
    private Interaction interaction = null;
    private InteractionUIBindings interactionUIBindings;
    private TextMeshProUGUI label;
    private Image icon;

    /// <summary>
    /// Called when the interaction Line prefab is added to the ui
    /// Sets the label and sprite values based on what the interaction is and how the user can trigger it
    /// </summary>
    /// <param name="interaction">The configured interaction of label, input method and onInteraction sequence</param>
    public void OnInitialise(Interaction interaction)
    {
        this.interaction = interaction;
        SetText();
        SetIcon(InputDeviceChangeHandler.currentDeviceType);
    }
    // Start is called before the first frame update
    private void Awake()
    {
        EventDispatcher.Instance.AddEventListener("OnInputDeviceChange",(Action<string>)SetIcon);
        interactionUIBindings = FindObjectOfType<InteractionUIBindings>();
        label = GetComponentInChildren<TextMeshProUGUI>();
        icon = GetComponentInChildren<Image>();
    }

    private void SetText()
    {
        // set the text
        label.text = interaction.label;
    }
    private void SetIcon(string deviceType)
    {
        if (this != null)
        {
            // set the sprite
            Sprite boundSprite = interactionUIBindings.GetSprite(interaction.method, deviceType);
            icon.sprite = boundSprite;
        }
    }
}
