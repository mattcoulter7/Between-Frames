using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FlashTriggerObject : MonoBehaviour
{
    public UnityEvent onFlashStart;
    public UnityEvent onFlashStop;
    public UnityEvent whileFlash;
    public float flashDuration = 5f;
    private bool _currentlyFlashed = false;
    private bool _currentlyVisible = false;
    private Coroutine _timer;
    // Start is called before the first frame update
    void Start()
    {
        // listen to the flash event
        EventDispatcher.AddEventListener("Flash", (Action<Image>)OnFlashStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentlyFlashed){
            WhileFlash();
        }
    }
    void WhileFlash(){
        whileFlash.Invoke();
    }
    void OnFlashStart(Image flashImage)
    {
        if (_currentlyVisible)
        {
            _currentlyFlashed = true;
            // do something
            onFlashStart.Invoke();
            _timer = StartCoroutine(OnFlashStop());
        }
    }

    IEnumerator OnFlashStop(){
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(flashDuration);
        onFlashStop.Invoke();
        StopCoroutine(_timer);
        _timer = null;
    }

    void OnBecameVisible()
    {
        _currentlyVisible = true;
    }

    void OnBecameInvisible()
    {
        _currentlyVisible = false;
    }
}
