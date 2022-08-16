using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class handles trigger the flash based on user input
/// It also controls the time in between flash
/// </summary>
public class FlashController : MonoBehaviour
{
    /// <summary>
    /// Reference to the flash class which handles to UI
    /// </summary>
    public Flash flash;

    /// <summary>
    /// Controls how long you have to wait in between flashes (seconds)
    /// </summary>
    public float timeBetweenFlash = 5f;

    /// <summary>
    /// Custom UnityEvent for what happens when the flash occurs (such as playing a flash sound)
    /// </summary>
    public UnityEvent onFlash;

    private Coroutine _instance = null;

    // Update is called once per frame
    private void Update()
    {
        if (_instance == null && Input.GetButtonDown("Flash"))
        {
            //Start the coroutine we define below named ExampleCoroutine.
            _instance = StartCoroutine(DoFlash());
        }
    }

    private IEnumerator DoFlash()
    {
        flash.doCameraFlash = true;
        onFlash.Invoke();

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeBetweenFlash);

        StopCoroutine(_instance);
        _instance = null;
    }
}
