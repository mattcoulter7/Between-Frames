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
    [SerializeField]
    private List<Sound> stepSounds;

    [SerializeField]
    private List<AudioMixerGroup> sfxGroups; 

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

        //adding footsteps to array
        Sound Step1 = GetSFX("Step1");
        Sound Step2 = GetSFX("Step2");
        stepSounds.Add(Step1);
        stepSounds.Add(Step2);



        foreach (Sound song in BGM)
        {
            song.source = gameObject.AddComponent<AudioSource>();
            song.source.clip = song.clip;
            song.source.volume = song.volume;
            song.source.pitch = song.pitch;
            song.source.loop = song.loop;
            song.source.playOnAwake = song.playOnAwake;
            song.source.volume = PlayerPrefs.GetFloat(BGMPref);

            if(song.mixerGroup == null) { song.mixerGroup = musicMixerGroup; }

            song.source.outputAudioMixerGroup = song.mixerGroup;
            
        }
        //BGM
        Debug.Log("vol shud now be  " + BGM[0].source.volume);

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

        foreach (Sound step in stepSounds)
        {
            step.mixerGroup = sfxGroups[0];
            step.source.outputAudioMixerGroup = step.mixerGroup;
        }
    }

    void Update()
    {
        
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

        EventDispatcher.Instance.AddEventListener("OnSceneLoad", (Action<Scene>)OnSceneLoad);
       
    }

    //<summary>This runs a method when the scene loads in game</summary>
    public void OnSceneLoad(Scene scene) {
        //play music
    }

    //<summary>This method updates all the tracks' volume in the BGM array</summary>
    public void UpdateBGMVol()
    {
        for (int i = 0; i < BGM.Length; i++)
        {
            BGM[i].source.volume = bgmLevel * BGM[i].volume;
        }
    }

    //<summary>This method updates all the tracks' volume in the SFX array</summary>
    public void UpdateSFXVol()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = sfxLevel * sounds[i].volume;      //eg. 0.5 x 0.5 = 0.25
        }
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
            Debug.Log("ARRAY POS " + i);

            if (!BGM[i].Equals(song) && BGM[i].source.isPlaying)
            {
                Debug.Log("BGM name: " + BGM[i].name);
                Debug.Log("Song name: " + song.name);
                BGM[i].source.Stop();
                Debug.Log("Something should have stopped");

            }

        }
        song.source.Play();

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
        for (int i = 0; i < sounds.Length - 1; i++)
        {
            sounds[i].source.Pause();
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
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        //s.source.volume = 0;
        s.source.Play();
        //StartCoroutine(FadeAudioSource.StartFade(s.source, (float)0.2, 1));
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

    //<summary>This plays a random range of footstep sounds. Consider making it able to play a range of whatever sounds</summary>
    public void PlayFootsteps()
    {
        Sound stepToPlay = stepSounds[UnityEngine.Random.Range(0, stepSounds.Count)];
        stepToPlay.source.Play();

    }

    //<summary>Ensures that the sound is not already playing before calling the play function</summary>
    //<param name="name">This is the name of the track to play</param>
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
}


//graveyard v2.0


////SliderContainer = GameObject.transform.Find(MusicContainer).gameObject;
////BGMSlider = SliderContainer.GetComponent<Slider>();



////firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

//if (firstPlayInt == 0)
//{
//    //BGMFloat = 1f;
//    //SFXFloat = 0.108f;

//    //BGMSlider.value = BGMFloat;
//    //SFXSlider.value = SFXFloat;
//   // PlayerPrefs.SetFloat(BGMPref, BGMFloat);
//   // PlayerPrefs.SetFloat(SFXPref, SFXFloat);
//   // PlayerPrefs.SetInt(FirstPlay, -1);
//}
//else
//{
//   // BGMFloat = PlayerPrefs.GetFloat(BGMPref);
//   // BGMSlider.value = BGMFloat;

//   // SFXFloat = PlayerPrefs.GetFloat(SFXPref);
//   // SFXSlider.value = SFXFloat;
//}




//Scene currentScene = SceneManager.GetActiveScene();
//string sceneName = currentScene.name;
//if (sceneName == "MenuTest")
//{
//    PlayBGM("MenuMusic");
//    Debug.Log("Menu should be playing");
//}
//else //(sceneName != "MenuTest")
//{

//    for (int i = 0; i < BGM.Length; i++)
//    {
//        if (BGM[i].source.isPlaying)
//        {

//            BGM[i].source.Stop();
//            Debug.Log("Something should have stopped");

//        }

//    }
//}