using UnityEngine;
using UnityEngine.UI;

public class VolControl : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BGMPref = "BGMPref";
    private static readonly string SFXPref = "SFXPref";
    private int firstPlayInt;

    [SerializeField] private Slider BGMSlider = null;
    [SerializeField] private Slider SFXSlider = null;
    private float BGMFloat, SFXFloat;

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

    public void SaveVol()
    {
        float volumeVal = BGMSlider.value;
        PlayerPrefs.SetFloat("VolVal", volumeVal);
        LoadValues();

        
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(BGMPref, BGMSlider.value);
        PlayerPrefs.SetFloat(SFXPref, SFXSlider.value);
    }
    
    public void UpdateBGMVol(Sound BGM)
    {
        BGM.volume = BGMSlider.value;
    }

    public void UpdateSFXVol(Sound[] sounds)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].volume = SFXSlider.value;
        }
    }


    // Just in case we  want to reset the  volume everytime the game is opened. Otherwise it will save even on exit.
    // public void ResetVol(){
    // 	PlayerPrefs.DeleteKey("VolVal");
    // }
    void LoadValues()
    {
        float volumeVal = PlayerPrefs.GetFloat("VolVal");
        BGMSlider.value = volumeVal;
        AudioListener.volume = volumeVal;
    }
}
