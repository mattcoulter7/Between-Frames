using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AM : MonoBehaviour
{
    public static readonly string FirstPlay = "FirstPlay";
    public static readonly string BGMPref = "BGMPref";
    public static readonly string SFXPref = "SFXPref";
    public static AM Instance;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;
    private int firstPlayInt;

    //private Scene currentScene;
    //private string sceneName = currentScene.name;

    private float bgmLevel;
    private float sfxLevel;

    public void setBGMLevel(float value)
    {
        bgmLevel = value;
        UpdateBGMVol();
        PlayerPrefs.SetFloat(BGMPref, value);
    }
    public void setSFXLevel(float value)
    {
        sfxLevel = value;
        UpdateSFXVol();
        PlayerPrefs.SetFloat(SFXPref, value);
    }
    public List<Sound> stepSounds;
    public Sound[] BGM;
    public Sound[] sounds;



    void Awake()
    {

        if (Instance != null) return;

        Instance = this;

        //adding footsteps to array
        Sound Step1 = GetSFX("Step1");
        Sound Step2 = GetSFX("Step2");
        stepSounds.Add(Step1);
        stepSounds.Add(Step2);
        //Debug.Log("Name: " + stepSounds[0].GetType());

        //Debug.Log("BGM Pref is " + PlayerPrefs.GetFloat(BGMPref));
        //Debug.Log("SFX Pref is " + PlayerPrefs.GetFloat(SFXPref));

        foreach (Sound song in BGM)
        {
            song.source = gameObject.AddComponent<AudioSource>();
            song.source.clip = song.clip;
            song.source.volume = song.volume;
            song.source.pitch = song.pitch;
            song.source.loop = song.loop;
            song.source.playOnAwake = song.playOnAwake;
            song.source.volume = PlayerPrefs.GetFloat(BGMPref);
            song.mixerGroup = musicMixerGroup;
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
            s.mixerGroup = sfxMixerGroup;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

       
    }

    void Update()
    {
        //currentScene = SceneManager.GetActiveScene();
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
        //setBGMLevel(PlayerPrefs.GetFloat(BGMPref));
        //setSFXLevel(PlayerPrefs.GetFloat(SFXPref));



        //foreach (Sound song in BGM)
        //{
        //    if (song.playOnAwake)
        //    {
        //        PlayBGM(song.name);
        //    }


        //}
        

        //foreach (Sound s in sounds)
        //{
        //    if (s.playOnAwake)
        //    {
        //        PlaySFX(s.name);
        //    }


        //}

        EventDispatcher.Instance.AddEventListener("OnSceneLoad", (Action<Scene>)OnSceneLoad);
       
    }

    public void OnSceneLoad(Scene scene) {
        //play music
    }


    public void SaveSettings()
    {
        //PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
        //PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }

    public void UpdateBGMVol()
    {
        for (int i = 0; i < BGM.Length; i++)
        {
            BGM[i].source.volume = bgmLevel * BGM[i].volume;
        }
    }

    public void UpdateSFXVol()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = sfxLevel * sounds[i].volume;      //eg. 0.5 x 0.5 = 0.25
        }
    }


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
            if (BGM[i].source.isPlaying)
            {
                Debug.Log("BGM name: " + BGM[i].name);
                Debug.Log("Song name: " + song.name);
                BGM[i].source.Stop();
                Debug.Log("Something should have stopped");
                
            }
            
        }
        song.source.Play();



    }
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

    // New Addition 12 Jul 2022
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

    public void PlayFootsteps()
    {
        Sound stepToPlay = (Sound)stepSounds[UnityEngine.Random.Range(0, stepSounds.Count)];
        stepToPlay.source.Play();
    }

    //Effects changing
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