using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUIBindings : MonoBehaviour
{
    [System.Serializable]
    public class Binding
    {
        public KeyCode keyCode;
        public Sprite sprite;
    }
    public List<Binding> bindings;

    public Sprite GetSprite(KeyCode keyCode)
    {
        foreach (Binding binding in bindings)
        {
            if (binding.keyCode == keyCode)
            {
                return binding.sprite;
            }
        }
        return null;
    }
}
