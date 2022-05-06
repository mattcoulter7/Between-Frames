using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AMAssist : MonoBehaviour
{
    private static readonly string BGMPref = "BGMPref";
    private static readonly string SFXPref = "SFXPref";

    [SerializeField] private Slider BGMSlider = null;
    [SerializeField] private Slider SFXSlider = null;

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
    public void InitBGMVol()
    {
        BGMSlider.value = PlayerPrefs.GetFloat(AM.BGMPref);
        //AM.BGMSlider.value = PlayerPrefs.GetFloat(AM.BGMPref);
    }

    public void InitSFXVol()
    {
        SFXSlider.value = PlayerPrefs.GetFloat(AM.SFXPref);
    }

    public void UpdateBGMVol(float value)
    {
        AM.Instance.setBGMLevel(value);
    }

    public void UpdateSFXVol(float value)
    {
        AM.Instance.setSFXLevel(value);
    }
}
