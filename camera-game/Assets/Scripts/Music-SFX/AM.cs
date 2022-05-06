using UnityEngine;
using UnityEngine.UI;
using System;


public class AM : MonoBehaviour
{
    public static readonly string FirstPlay = "FirstPlay";
    public static readonly string BGMPref = "BGMPref";
    public static readonly string SFXPref = "SFXPref";
    public static AM Instance;

    private int firstPlayInt;

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


    public Sound[] BGM;
    public Sound[] sounds;

    void Awake()
    {
        Instance = this;
        Debug.Log("BGM Pref is " + PlayerPrefs.GetFloat(BGMPref));
        Debug.Log("SFX Pref is " + PlayerPrefs.GetFloat(SFXPref));

        foreach (Sound song in BGM)
        {
            song.source = gameObject.AddComponent<AudioSource>();
            song.source.clip = song.clip;
            song.source.volume = song.volume;
            song.source.pitch = song.pitch;
            song.source.loop = song.loop;
            song.source.playOnAwake = song.playOnAwake;
            song.source.volume = PlayerPrefs.GetFloat(BGMPref);

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
        }
    }

    void Update()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        setBGMLevel(PlayerPrefs.GetFloat(BGMPref));
        setSFXLevel(PlayerPrefs.GetFloat(SFXPref));

       
        foreach (Sound song in BGM)
        {
            if (song.playOnAwake)
            {
                PlayBGM(song.name);
            }


        }
        

        foreach (Sound s in sounds)
        {
            if (s.playOnAwake)
            {
                PlaySFX(s.name);
            }


        }
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
            BGM[i].source.volume = bgmLevel;
        }
    }

    public void UpdateSFXVol()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = sfxLevel;
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