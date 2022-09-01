using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Handles the visualisation of a camera flash
/// It works by lerping a white image opacity from 100% to 0%
/// </summary>
public class Flash : MonoBehaviour
{
    /// <summary>
    /// The duration of the flash
    /// </summary>
    public float flashTimelength = .2f;
    /// <summary>
    /// True will trigger a flash to happen
    /// </summary>
    public bool doCameraFlash = false;

    private Image flashImage;
    private float startTime;
    private bool flashing = false;

    /// <summary>
    /// Trigger the camera flash to happen
    /// </summary>
    public void CameraFlash()
    {
        // initial color
        Color col = flashImage.color;

        // start time to fade over time
        startTime = Time.time;

        // so we can flash again
        doCameraFlash = false;

        // start it as alpha = 1.0 (opaque)
        col.a = 1.0f;

        // flash image start color
        flashImage.color = col;

        // flag we are flashing so user can't do 2 of them
        flashing = true;

        StartCoroutine(FlashCoroutine());
        EventDispatcher.Instance.Dispatch("Flash", flashImage);
    }
    private void Start()
    {
        flashImage = GetComponent<Image>();
        flashImage.enabled = true;
        Color col = flashImage.color;
        col.a = 0.0f;
        flashImage.color = col;
    }
    private void Update()
    {
        if (doCameraFlash && !flashing)
        {
            CameraFlash();
        }
        else
        {
            doCameraFlash = false;
        }
    }

    private IEnumerator FlashCoroutine()
    {
        bool done = false;

        while (!done)
        {
            float perc;
            Color col = flashImage.color;

            perc = Time.time - startTime;
            perc = perc / flashTimelength;

            if (perc > 1.0f)
            {
                perc = 1.0f;
                done = true;
            }

            col.a = Mathf.Lerp(1.0f, 0.0f, perc);
            flashImage.color = col;
            flashing = true;

            yield return null;
        }

        flashing = false;

        yield break;
    }
}