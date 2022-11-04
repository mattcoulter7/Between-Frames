using UnityEngine.Audio;
using UnityEngine;

/// <summary>This is a custom sound class that has a name, Audioclip, A mixergroup, volume, pitch, loop boolean, a play on awake bool and an audiosource</summary>
[System.Serializable]
public class Sound{

    /// <summary>This is the name of the sound</summary>
	public string name;

    /// <summary>This is the audio clip of the sound</summary>
    public AudioClip clip;

    /// <summary>This is the mixer group the sound will belong to and be controlled by. This is given on initialisation</summary>
    public AudioMixerGroup mixerGroup;

    /// <summary>This is the volume of the sound</summary>
    [Range(0f, 1f)]
    public float volume = 1f;

    /// <summary>This is the pitch of the sound</summary>
    [Range(.1f, 3f)]
    public float pitch = 1f;

    /// <summary>This bool will loop the sound when it is enabled.</summary>
    public bool loop = false;

    /// <summary>This bool will stop the looping when the scene loads.</summary>
    public bool stopLoopOnAwake = true;

    /// <summary>This bool will play the sound on awake when enabled</summary>
    public bool playOnAwake = false;

    /// <summary>This holds an audio source of the sound</summmary>
    [HideInInspector]
    public AudioSource source;
}
