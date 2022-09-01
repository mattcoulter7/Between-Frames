using System.Collections;
using UnityEngine;

/// <summary>This class is an attempt to fade Sounds in and out.
public static class FadeAudioSource
{
    /// <summary>This method is what will start the lerp from the current Sound's volume to the target volume, lasting however long the duration is</summary>
    /// <param name="audioSource">This is the AudioSource whos volume will be changed</param>
    /// <param name="duration">This float is how long the fade will last for</param>
    /// <param name="targetVolume">This float is what the Audio Source volume will end up at when the fade finishes</param>
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        //yield break;
    }
}