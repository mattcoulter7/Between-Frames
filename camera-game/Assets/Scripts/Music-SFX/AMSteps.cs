using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMSteps : MonoBehaviour
{
    public Sound[] LinolSteps;
    public Sound[] WoodSteps;
    public Sound[] CarpetSteps;
    public Sound[] MetalSteps;
    public Sound[] BarSteps;
    public Sound[] GrassSteps;

    private void Awake()
    {

        foreach (Sound s in LinolSteps)
        {
            Init(s);
        }
        foreach (Sound s in WoodSteps)
        {
            Init(s);
        }
        foreach (Sound s in CarpetSteps)
        {
            Init(s);
        }
        foreach (Sound s in MetalSteps)
        {
            Init(s);
        }
        foreach (Sound s in BarSteps)
        {
            Init(s);
        }
        foreach (Sound s in GrassSteps)
        {
            Init(s);
        }


    }

    void Init(Sound s)
    {
        if (s != null)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

            
        }
    }

    //foreach (Sound[] soundArray in AMSteps) //sad--doesnt work
    //{
    //    if (soundArray != null)
    //    {
    //        foreach (Sound s in soundArray)
    //        {
    //            if (s != null)
    //            {
    //                s.source = gameObject.AddComponent<AudioSource>();
    //                s.source.clip = s.clip;
    //                s.source.volume = s.volume;
    //                s.source.pitch = s.pitch;
    //                s.source.loop = s.loop;
    //                s.source.playOnAwake = s.playOnAwake;

    //                if (s.mixerGroup == null) { s.mixerGroup = AM.Instance.sfxGroups[0]; }
    //                s.source.outputAudioMixerGroup = s.mixerGroup;
    //            }
    //            return;
    //        }
    //    }
    //    return;

    //}
}
