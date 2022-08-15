using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

//<summary>This class will handle connecting any sliders to the Audio Manager and the Mixer/Sub-mixer groups
//as well as changing the volumes of the BGMs or SFXs according to the changing of the slider values</summary>
public class AMAssist : MonoBehaviour
{
    private static readonly string BGMPref = "BGMPref";
    private static readonly string SFXPref = "SFXPref";

    //<summary>This is the Unity audio mixer for all the sounds and acts as the main volume</summary>
    [SerializeField] AudioMixer mixer;

    //<summary>This will hold a slider that changes the volume of all the BGMs</summary>
    [SerializeField] private Slider BGMSlider = null;

    //<summary>This will hold a slider that changes the volume of all the SFXs</summary>
    [SerializeField] private Slider SFXSlider = null;

    //<summary>This constant string holds the name of the sub-mixer group for all the BGMs</summary>
    const string MIXER_MUSIC = "MusicVolume";

    //<summary>This constant string holds the name of the sub-mixer group for all the SFXs</summary>
    const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    //<summary>This method will change the sub-group mixer value to a log number that never reaches 0.
    //This ensures that the volume reaches levels quiet enough its unheard, but won't reach 0 to avoid reaching 0 decibels.</summary>
    //<param name="value">This is the the user wishes the music to be changed to</param>
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    //<summary>This method will change the sub-group mixer value to a log number that never reaches 0.
    //This ensures that the volume reaches levels quiet enough its unheard, but won't reach 0 to avoid reaching 0 decibels.</summary>
    //<param name="value">This is the the user wishes the SFX to be changed to</param>
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    private void Start()
    {
        UpdateBGMVol(PlayerPrefs.GetFloat(AM.BGMPref));
        UpdateSFXVol(PlayerPrefs.GetFloat(AM.SFXPref));
        BGMSlider.value = PlayerPrefs.GetFloat(AM.BGMPref);
        SFXSlider.value = PlayerPrefs.GetFloat(AM.SFXPref);
        //if (PlayerPrefs.HasKey("VolVal"))
        //{
        //    LoadValues();
        //}
        //else
        //{
        //    volSlider.value = 1.0f;
        //}
        //Debug.Log("vol is:" + volSlider.value);
    }

    //<summary>This method saves the settings of the BGM and SFX sliders and volumes to carry over between scenes and game sessions.</summary>
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }


    void Update()
    {
        //BGM.volume = BGM.source.volume;
        //BGM.pitch = BGM.source.pitch;
        //BGM.loop = BGM.source.loop;
        //BGM.playOnAwake = BGM.source.playOnAwake;

        //foreach (Sound s in sounds)
        //{

        //    s.source.volume = s.volume;
        //    s.source.volume = SFXSlider.value;
        //    s.source.pitch = s.pitch;
        //    s.source.loop = s.loop;
        //    s.source.playOnAwake = s.playOnAwake;
        //}
    }

    //<summary>This method initialises the BGM slider value to whatever is saved in the BGMPref</summary>
    public void InitBGMVol()
    {
        BGMSlider.value = PlayerPrefs.GetFloat(AM.BGMPref);
    }

    //<summary>This method initialises the BGM slider value to whatever is saved in the SFXPref</summary>
    public void InitSFXVol()
    {
        SFXSlider.value = PlayerPrefs.GetFloat(AM.SFXPref);
    }

    //<summary>This method gives a value to the AM which will be used to change all the BGM volumes accordingly</summary>
    //<param name="value">This is the value which will be passed to the AM</param>
    public void UpdateBGMVol(float value)
    {
        AM.Instance.setBGMLevel(value);
    }

    //<summary>This method gives a value to the AM which will be used to change all the SFX volumes accordingly</summary>
    //<param name="value">This is the value which will be passed to the AM</param>
    public void UpdateSFXVol(float value)
    {
        AM.Instance.setSFXLevel(value);
    }
}
