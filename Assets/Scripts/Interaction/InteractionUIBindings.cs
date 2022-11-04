using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class enabled assigning UI sprites to key inputs for the interaction system
/// Doing so, the user is able to see the key to press to trigger an interaction
/// </summary>
public class InteractionUIBindings : MonoBehaviour
{
    /// <summary>
    /// This class defines the link between a key code and the sprite UI for that key
    /// </summary>
    [System.Serializable]
    public class Binding
    {
        /// <summary>
        /// The key code from input manager
        /// </summary>
        public string keyCode;

        /// <summary>
        /// The sprite which represents that input method
        /// </summary>
        public Sprite sprite;

        /// <summary>
        /// The sprite which represents that input method for controller
        /// </summary>
        public Sprite controllerSprite;
    }

    /// <summary>
    /// The list of bindings between key input and sprite representation (because dictionaries don't show in the inspector!)
    /// </summary>
    public List<Binding> bindings;

    /// <summary>
    /// Get the sprite based on a key code
    /// </summary>
    /// <param name="keyCode">The InputManager key identifier</param>
    /// <returns>The sprite that represents the key, as long as it has been assigned in the inspector</returns>
    public Sprite GetSprite(string keyCode,string deviceType)
    {
        foreach (Binding binding in bindings)
        {
            if (binding.keyCode == keyCode)
            {
                return deviceType == "Gamepad" ? binding.controllerSprite : binding.sprite;
            }
        }
        return null;
    }
}
