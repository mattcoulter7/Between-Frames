using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>This class holds all the methods that can play Sounds in different ways</summary>
public class PlaySound : MonoBehaviour
{
    private bool startedPlaying = false;

    /// <summary>This method calls the play SFX method from the singleton</summary>
    /// <param name="name">This is the specific sound to play</param>
    public void Play(string name)
    {

        AM.Instance.PlaySFX(name);
        //Debug.Log("Played sound from PlaySound");
    }

    /// <summary>This method calls the stop SFX method from the singleton</summary>
    /// <param name="name">This is the specific sound to stop</param>
    public void Stop(string name)
    {

        AM.Instance.StopSFX(name);
        
    }

    /// <summary>This method was intended to play a looped sound if it hasn't started playing.</summary>
    /// <param name="name">This is the specific sound to play</param>
    public void PlayLooped(string name)
    {

        if (!startedPlaying)
        {
            startedPlaying = true;
            AM.Instance.PlaySFX(name);
            startedPlaying = false;
        }
        //else
        //{
        //    return;
        //}
       
    }

    /// <summary>Ensures the sound only plays once</summary>
    /// <param name="name">This is the specific sound to play</param>
    public void PlayOnce(string name)
    {
        //startedPlaying = true;
        if(!startedPlaying)
        {
            AM.Instance.PlaySFX(name);
            startedPlaying = true;
        }

    }

    /// <summary>Ensures the sound finishes playing before calling the play function againe</summary>
    /// <param name="name">This is the specific sound to play</param>
    public void PlayWholeClip(string name)
    {
        AM.Instance.PlayWholeClip(name);
    }

    /// <summary>This method stops all looped sounds</summary>
    // <param name="name">This is the specific sound to stop</param>
    public void StopLooped()
    {
        //startedPlaying = false;
        AM.Instance.StopLooped();

    }

    /// <summary>This was made to play the footsteps by cycling through the footstep sound array randomly</summary>
    public void PlaySteps()
    {
        //AM.Instance.PlayFootsteps();
    }

    /// <summary>Fading into a loop (hopefully)</summary>
    /// <param name="name">This is the specific sound to fade in</param>
    public void FadeInLoop(string name)
    {
        AM.Instance.PlayFadeIn(name);
    }

    /// <summary>Fading out of a loop</summary>
    /// <param name="name">This is the specific sound to fade out</param>
    public void FadeOutLoop(string name)
    {
        AM.Instance.StopFadeOut(name);
    }

    public void PauseAllSFX()
    {
        AM.Instance.PauseAllSFX();
    }

}
