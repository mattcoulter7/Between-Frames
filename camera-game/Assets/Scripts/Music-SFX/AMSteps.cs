using UnityEngine;

/// <summary>
/// Class that handles all the footstep sounds in the game.
/// </summary>
public class AMSteps : MonoBehaviour
{
    /// <summary>
    /// array of steps for the linoleum/stone tag
    /// </summary>
    public Sound[] LinolSteps;

    /// <summary>
    /// array of steps for the wood tag
    /// </summary>
    public Sound[] WoodSteps;

    /// <summary>
    /// array of steps for the carpet tag
    /// </summary>
    public Sound[] CarpetSteps;

    /// <summary>
    /// array of steps for the metal tag
    /// </summary>
    public Sound[] MetalSteps;

    /// <summary>
    /// array of steps for the Bar tag
    /// </summary>
    public Sound[] BarSteps;

    /// <summary>
    /// array of steps for the grass tag
    /// </summary>
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

    /// <summary>
    /// initialises the sounds in the same way on Awake in the Audio Manager
    /// </summary>
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
            if (s.mixerGroup != null) s.source.outputAudioMixerGroup = s.mixerGroup;
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
