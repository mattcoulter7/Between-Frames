using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class enables easy Instantiations of prefabs to be called as a Inspector configuration, such as UnityEvent
/// </summary>
public class PrefabFactory : MonoBehaviour
{
    public void InstantiatePrefab(GameObject prefab)
    {
        GameObject instance = Instantiate(prefab, transform);
    }
}
