using UnityEngine;
using UnityEngine.UI;
using System;


public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BGMPref = "BGMPref";
    private static readonly string SFXPref = "SFXPref";
    private int firstPlayInt;

    [SerializeField] private Slider BGMSlider = null;
    [SerializeField] private Slider SFXSlider = null;
    private float BGMFloat, SFXFloat;

    public Sound BGM;
    public Sound[] sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        DontDestroyOnLoad(gameObject);

        BGM.source = gameObject.AddComponent<AudioSource>();
        BGM.source.clip = BGM.clip;
        BGM.source.volume = BGM.volume;
        BGM.source.pitch = BGM.pitch;
        BGM.source.loop = BGM.loop;
        BGM.source.playOnAwake = BGM.playOnAwake;

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
        BGM.volume = BGM.source.volume;
        BGM.pitch = BGM.source.pitch;
        BGM.loop = BGM.source.loop;
        BGM.playOnAwake = BGM.source.playOnAwake;

        foreach (Sound s in sounds)
        {
           
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }

    }


    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            BGMFloat = 1f;
            SFXFloat = 0.108f;

            BGMSlider.value = BGMFloat;
            SFXSlider.value = SFXFloat;
            PlayerPrefs.SetFloat(BGMPref, BGMFloat);
            PlayerPrefs.SetFloat(SFXPref, SFXFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            BGMFloat = PlayerPrefs.GetFloat(BGMPref);
            BGMSlider.value = BGMFloat;

            SFXFloat = PlayerPrefs.GetFloat(SFXPref);
            SFXSlider.value = SFXFloat;
        }

        if(BGM.playOnAwake)
        {
            PlayBGM();
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
        PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }

    public void UpdateBGMVol()
    {
        BGM.source.volume = BGMSlider.value;
    }

    public void UpdateSFXVol()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = SFXSlider.value;
        }
    }

    public void PlayBGM()
    {
        BGM.source.Play();
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
