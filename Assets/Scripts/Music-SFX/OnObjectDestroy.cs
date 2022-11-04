using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnObjectDestroy : MonoBehaviour
{
    public UnityEvent whenDestroyed;
    public void OnDestroy()
    {
        whenDestroyed.Invoke();
    }
}
