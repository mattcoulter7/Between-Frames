using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// helper functions to easily set children based on interaction object
/// </summary>
public class InteractionMenuLine : MonoBehaviour
{
    private TextMeshProUGUI label;
    private Image icon;

    /// <summary>
    /// Called when the interaction Line prefab is added to the ui
    /// Sets the label and sprite values based on what the interaction is and how the user can trigger it
    /// </summary>
    /// <param name="interaction">The configured interaction of label, input method and onInteraction sequence</param>
    public void OnInitialise(Interaction interaction)
    {
        InteractionUIBindings interactionUIBindings = GetComponentInParent<InteractionUIBindings>();

        // set the text
        label.text = interaction.label;

        // set the sprite
        Sprite boundSprite = interactionUIBindings.GetSprite(interaction.method);
        icon.sprite = boundSprite;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        icon = GetComponentInChildren<Image>();
    }
}
