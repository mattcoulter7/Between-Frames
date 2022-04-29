using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// helper functions to easily set children based on interaction object
public class InteractionMenuLine : MonoBehaviour
{
    private TextMeshProUGUI label;
    private Image icon;
    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        icon = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    public void OnInitialise(Interaction interaction)
    {
        InteractionUIBindings interactionUIBindings = GetComponentInParent<InteractionUIBindings>();

        // set the text
        label.text = interaction.label;
        
        // set the sprite
        Sprite boundSprite = interactionUIBindings.GetSprite(interaction.method);
        icon.sprite = boundSprite;
    }
}
