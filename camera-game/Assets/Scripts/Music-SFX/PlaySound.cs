using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private bool startedPlaying = false;
    private bool soundPlayed;
    private void Start()
    {
        soundPlayed = false;
    }
    public void Play(string name)
    {

        AM.Instance.PlaySFX(name);
        //Debug.Log("Played sound from PlaySound");
    }

    public void Stop(string name)
    {

        AM.Instance.StopSFX(name);
        //Debug.Log("Played sound from PlaySound");
    }

    public void PlayLooped(string name)
    {
        if (!startedPlaying)
        {
            AM.Instance.PlaySFX(name);
            startedPlaying = true;
        }
        else
        {
            return;
        }
       
    }

    //Ensures the sound only plays once
    public void PlayOnce(string name)
    {
        //startedPlaying = true;
        if(!startedPlaying)
        {
            AM.Instance.PlaySFX(name);
            startedPlaying = true;
        }


    }

    public void StopLooped(string name)
    {
        startedPlaying = false;
        AM.Instance.StopSFX(name);

    }

    public void PlaySteps()
    {
        AM.Instance.PlayFootsteps();
    }

    //Fading in and out of a loop (hopefully)
    public void FadeInLoop(string name)
    {
        AM.Instance.PlayFadeIn(name);
    }

    public void FadeOutLoop(string name)
    {
        AM.Instance.StopFadeOut(name);
    }


}
