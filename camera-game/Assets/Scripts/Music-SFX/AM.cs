using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections.Generic;

//<summary>This Audio Manager class holds all the sound effects in the game.
//It also holds the methods to play the sound effects and the audio mixer groups </summary>

public class AM : MonoBehaviour
{
    //<summary>String to save key for setting initial sound settings upon first play</summary>
    public static readonly string FirstPlay = "FirstPlay";

    //<summary>String to save key for saving BGM vol settings</summary>
    public static readonly string BGMPref = "BGMPref";

    //<summary>String to save key for saving SFX vol settings</summary>
    public static readonly string SFXPref = "SFXPref";

    //<summary>This makes the AM a singleton</summary>
    public static AM Instance;

    /// <summary>
    /// to set on awake volumes
    /// </summary>
    [SerializeField] AudioMixer mainMixer;

    /// <summary>This constant string holds the name of the sub-mixer group for all the BGMs</summary>
    const string MIXER_MUSIC = "MusicVolume";

    /// <summary>This constant string holds the name of the sub-mixer group for all the SFXs</summary>
    const string MIXER_SFX = "SFXVolume";

    [SerializeField]
    private AudioMixer sfxMixer;

    //<summary>The mixer group for all music tracks</summary>
    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    //<summary>The mixer group for all SFX tracks</summary>
    [SerializeField]
    private AudioMixerGroup sfxMixerGroup;

    //<summary>Sound array for all the background music in the game</summary>
    public Sound[] BGM;

    //<summary>This is seen in the inspector for testing purposes. It checks which footstep sounds are
    // being put in the new array to be played later</summary>
    //[SerializeField]
    private AMSteps stepSounds;
    private BGMSequencer sequence;

    //[SerializeField]
    public List<AudioMixerGroup> sfxGroups;

    const string FADE_GROUP = "TestFadeVol";

    //<summary>Sound array for all the sound effects in the game/summary>
    public Sound[] sounds;

    private int firstPlayInt;

    private float bgmLevel;
    private float sfxLevel;

    //<summary>This method sets the BGM level</summary>
    //<param name="value">contains the number from the BGM vol slider which will change all the BGM track volumes</param>
    public void setBGMLevel(float value)
    {
        bgmLevel = value;
        UpdateBGMVol();
        PlayerPrefs.SetFloat(BGMPref, value);
    }

    //<summary>This method sets the SFX level</summary>
    //<param name="value">contains the number from the SFX vol slider which will change all the SFX track volumes</param>
    public void setSFXLevel(float value)
    {
        sfxLevel = value;
        UpdateSFXVol();
        PlayerPrefs.SetFloat(SFXPref, value);
    }

    void Awake()
    {

        if (Instance != null) return;

        Instance = this;

        stepSounds = GetComponent<AMSteps>();
        sequence = GetComponent<BGMSequencer>();
        InitSteps();


        foreach (Sound song in BGM)
        {
            song.source = gameObject.AddComponent<AudioSource>();
            song.source.clip = song.clip;
            song.source.volume = song.volume;
            song.source.pitch = song.pitch;
            song.source.loop = song.loop;
            song.source.playOnAwake = song.playOnAwake;

            if(song.mixerGroup == null) { song.mixerGroup = musicMixerGroup; }

            song.source.outputAudioMixerGroup = song.mixerGroup;
            
        }
        
        //Debug.Log("vol shud now be  " + BGM[0].source.volume);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

            if (s.mixerGroup == null) { s.mixerGroup = sfxMixerGroup; }
            
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            bgmLevel = 1f;
            sfxLevel = 1f;

            setBGMLevel(bgmLevel);
            setSFXLevel(sfxLevel);

            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            setBGMLevel(PlayerPrefs.GetFloat(BGMPref));
            setSFXLevel(PlayerPrefs.GetFloat(SFXPref));
        }

        //EventDispatcher.Instance.AddEventListener("OnSceneLoad", (Action<Scene>)OnSceneLoad);
       
    }

    //<summary>This runs a method when the scene loads in game</summary>
    public void OnSceneLoad(Scene scene) {
        //play music
    }

    //<summary>This method updates all the tracks' volume in the BGM array</summary>
    public void UpdateBGMVol()
    {
        mainMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(PlayerPrefs.GetFloat(BGMPref)) * 20);
        Debug.Log("MX SFX: " + PlayerPrefs.GetFloat(BGMPref));
        /*for (int i = 0; i < BGM.Length; i++)
        {
            BGM[i].source.volume = bgmLevel * BGM[i].volume;
        }*/
    }

    //<summary>This method updates all the tracks' volume in the SFX array</summary>
    public void UpdateSFXVol()
    {
        mainMixer.SetFloat(MIXER_SFX, Mathf.Log10(PlayerPrefs.GetFloat(SFXPref)) * 20);
        Debug.Log("MX SFX: " + PlayerPrefs.GetFloat(SFXPref));
        /*for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = sfxLevel * sounds[i].volume;      //eg. 0.5 x 0.5 = 0.25
        }*/
    }

    //<summary>This method finds the specific BGM to play from the array, stops any other tracks, then plays the desired track</summary>
    //<param name="name">This is the specific string that carries the name of the track to play</param>
    public void PlayBGM(string name)
    {
        Sound song = Array.Find(BGM, sound => sound.name == name);
        if (song == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }

        for (int i = 0; i < BGM.Length; i++)
        {
            //Debug.Log("ARRAY POS " + i);

            if (!BGM[i].Equals(song) && BGM[i].source.isPlaying)
            {
                Debug.Log("BGM name: " + BGM[i].name);
                Debug.Log("Song name: " + song.name);
                BGM[i].source.Stop();
                Debug.Log("Something should have stopped");

            }

        }
        if (!song.source.isPlaying)
            song.source.Play();
    }

    public void StopBGM(string name)
    {
        Sound song = Array.Find(BGM, sound => sound.name == name);
        if (song == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }

        song.source.Stop();

    }

    public Sound GetBGM(string name)
    {
        Sound song = Array.Find(BGM, sound => sound.name == name);
        if (song == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return null;
        }

        return song;

    }

    public void StartSequence()
    {
        sequence.OnStart();

    }


    //<summary>This method finds the specific SFX to play from the array, then plays the desired track</summary>
    //<param name="name">This is the specific string that carries the name of the track to play</param>
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) 
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        s.source.Play();
    }

    //<summary>This method finds the specific track within the array to stop</summary>
    //<param name="name">This is the specific string that carries the name of the track to stop</param>
    public void StopSFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        s.source.Stop();
    }

    //<summary>This method finds the specific track within the array to pause</summary>
    //<param name="name">This is the specific string that carries the name of the track to pause</param>
    public void PauseSFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        s.source.Pause();
    }

    //<summary>This method pauses all the tracks in the SFX array</summary>
    public void PauseAllSFX()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.Pause();
        }
    }

    //<summary>This method stops any looping tracks in the SFX array</summary>
    public void StopLooped()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].loop && sounds[i].stopLoopOnAwake)
            {
                sounds[i].source.Pause();
            }
        }
    }

    //<summary>This method returns a Sound object with the desired name. Returns null if nothing found</summary>
    //<returns>Returns a Sound object with the desired name. Returns null if nothing found</returns>
    //<param name="name">The name of the SFX to return</param>
    public Sound GetSFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return null;
        }
        else
        {
            return s;
        }
    }

    ///////////////////////////////////New Addition 12 Jul 2022////////////////////////

    //<summary>This method was intended to fade in a SFX track</summary>
    //<param name="name">This is the name of the track to fade in and play</param>
    public void PlayFadeIn(string name)
    {
        //Debug.Log("Called!");
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        if (s.source.isPlaying) return;
        sfxMixer.SetFloat(FADE_GROUP, -80f);
        s.source.Play();
        StartCoroutine(FadeMixerGroup.StartFade(sfxMixer, FADE_GROUP, 1f, 1f));
    }

    //<summary>This method was intended to fade out a SFX track</summary>
    //<param name="name">This is the name of the track to fade out and stop</param>
    public void StopFadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }

        //float.Equals()
        
        //StartCoroutine(FadeAudioSource.StartFade(s.source, 5, 0));
        //StartCoroutine(FadeAudioSource.StartFade(s.source, 2, (float)0.0001));

        //s.source.Stop();
        
    }





    /// <summary>
    /// This plays a random range of footstep sounds. Consider making it able to play a range of whatever sounds
    /// </summary>
    /// <param name="material"></param>
    public void PlayFootsteps(SurfaceMaterial material)
    {
        if (material != null)
        {
            Sound[] stepArray = material.identity switch
            {
                SurfaceIdentity.Wood => stepSounds.WoodSteps,
                SurfaceIdentity.Carpet => stepSounds.CarpetSteps,
                SurfaceIdentity.Metal => stepSounds.MetalSteps,
                SurfaceIdentity.Bars => stepSounds.BarSteps,
                SurfaceIdentity.Grass => stepSounds.GrassSteps,
                _ => stepSounds.LinolSteps,
            };
            stepArray[UnityEngine.Random.Range(0, stepArray.Length)].source.Play();

        }
        else if (material == null)
        {
            Sound stepToPlay = stepSounds.LinolSteps[UnityEngine.Random.Range(0, stepSounds.LinolSteps.Length)];
            Debug.LogWarning("No material assigned or material is null");
            stepToPlay.source.Play();
        }
    }

    

    ///<summary>Ensures that the sound is not already playing before calling the play function</summary>
    ///<param name="name">This is the name of the track to play</param>
    public void PlayWholeClip(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }

        if (!s.source.isPlaying)
        {
            s.source.Play();
        }

    }

    ////<summary>Effects changing for sfx. Was intended for something....</summary>
    public void highPassFilter()
    {
        //sfxMixerGroup.
    }

    /// <summary>
    /// Initialises the stepsounds in regards to their audiogroups.
    /// </summary>
    private void InitSteps()
    {
        foreach (Sound s in stepSounds.LinolSteps)
        {
            s.mixerGroup = sfxGroups[0];
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        foreach (Sound s in stepSounds.WoodSteps)
        {
            s.mixerGroup = sfxGroups[0];
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
        foreach (Sound s in stepSounds.CarpetSteps)
        {
            s.mixerGroup = sfxGroups[0];
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
        foreach (Sound s in stepSounds.GrassSteps)
        {
            s.mixerGroup = sfxGroups[0];
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

    }

}
