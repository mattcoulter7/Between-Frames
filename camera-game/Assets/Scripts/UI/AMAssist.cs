using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

/// <summary>This class will handle connecting any sliders to the Audio Manager and the Mixer/Sub-mixer groups
/// as well as changing the volumes of the BGMs or SFXs according to the changing of the slider values</summary>
public class AMAssist : MonoBehaviour
{
    private static readonly string BGMPref = "BGMPref";
    private static readonly string SFXPref = "SFXPref";

    /// <summary>This is the Unity audio mixer for all the sounds and acts as the main volume</summary>
    [SerializeField] AudioMixer mixer;

    /// <summary>This will hold a slider that changes the volume of all the BGMs</summary>
    [SerializeField] private Slider BGMSlider = null;

    /// <summary>This will hold a slider that changes the volume of all the SFXs</summary>
    [SerializeField] private Slider SFXSlider = null;

    /// <summary>This constant string holds the name of the sub-mixer group for all the BGMs</summary>
    const string MIXER_MUSIC = "MusicVolume";

    /// <summary>This constant string holds the name of the sub-mixer group for all the SFXs</summary>
    const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    /// <summary>This method will change the sub-group mixer value to a log number that never reaches 0.
    //This ensures that the volume reaches levels quiet enough its unheard, but won't reach 0 to avoid reaching 0 decibels.</summary>
    /// <param name="value">This is the vol the user wishes the music to be changed to</param>
    public void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
        //changes the slider and saves it to pref
        AM.Instance.setBGMLevel(value);
       
    }

    /// <summary>This method will change the sub-group mixer value to a log number that never reaches 0.
    //This ensures that the volume reaches levels quiet enough its unheard, but won't reach 0 to avoid reaching 0 decibels.</summary>
    /// <param name="value">This is the vol the user wishes the SFX to be changed to</param>
    public void SetSFXVolume(float value)
    {
        //changes the slider and saves it to pref
        AM.Instance.setSFXLevel(value);

        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
        
    }

    private void Start()
    {
        //UpdateBGMVol(PlayerPrefs.GetFloat(AM.BGMPref));
        //UpdateSFXVol(PlayerPrefs.GetFloat(AM.SFXPref));
        SetMusicVolume(PlayerPrefs.GetFloat(AM.BGMPref));
        SetSFXVolume(PlayerPrefs.GetFloat(AM.SFXPref));
        BGMSlider.value = PlayerPrefs.GetFloat(AM.BGMPref);
        SFXSlider.value = PlayerPrefs.GetFloat(AM.SFXPref);
        
       
    }

    /// <summary>This method saves the settings of the BGM and SFX sliders and volumes to carry over between scenes and game sessions.</summary>
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }

    /// <summary>This method initialises the BGM slider value to whatever is saved in the BGMPref</summary>
    public void InitBGMVol()
    {
        //BGMSlider.value = mixer.
        BGMSlider.value = PlayerPrefs.GetFloat(AM.BGMPref);
    }

    /// <summary>This method initialises the BGM slider value to whatever is saved in the SFXPref</summary>
    public void InitSFXVol()
    {
        SFXSlider.value = PlayerPrefs.GetFloat(AM.SFXPref);
    }

    /// <summary>This method gives a value to the AM which will be used to change all the BGM volumes accordingly</summary>
    /// <param name="value">This is the value which will be passed to the AM</param>
    /*public void UpdateBGMVol(float value)
    {
        //AM.Instance.setBGMLevel(value);
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
    }

    /// <summary>This method gives a value to the AM which will be used to change all the SFX volumes accordingly</summary>
    /// <param name="value">This is the value which will be passed to the AM</param>
    public void UpdateSFXVol(float value)
    {
        AM.Instance.setSFXLevel(value);
    }*/
}
