using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUIBindings : MonoBehaviour
{
    [System.Serializable]
    public class ActionBinding
    {
        public string action;
        public GameObject prefab;
    }
    public List<ActionBinding> actionUIBindings = new();
    public GameObject GetPrefab(string action)
    {
        foreach (ActionBinding binding in actionUIBindings)
        {
            if (binding.action == action)
            {
                return binding.prefab;
            }
        }
        return null;
    }
}
