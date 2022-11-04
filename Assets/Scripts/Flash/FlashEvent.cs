using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlashEvent : MonoBehaviour
{
    public UnityEvent onFlash;

    public void playFlashFX()
    {
        onFlash.Invoke();
    }
    
}
