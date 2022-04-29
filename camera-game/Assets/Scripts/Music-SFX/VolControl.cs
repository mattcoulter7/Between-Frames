using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolControl : MonoBehaviour
{
    [SerializeField] private Slider volSlider = null;
    //[SerializeField] private Slider sfxSlider = null;

    void Start()
    {
        if (PlayerPrefs.HasKey("VolVal"))
        {
            LoadValues();
        }
        else
        {
            volSlider.value = 1.0f;
        }
        Debug.Log("vol is:" + volSlider.value);
    }

    public void SaveVol()
    {
        float volumeVal = volSlider.value;
        PlayerPrefs.SetFloat("VolVal", volumeVal);
        LoadValues();
    }

    // Just in case we  want to reset the  volume everytime the game is opened. Otherwise it will save even on exit.
    // public void ResetVol(){
    // 	PlayerPrefs.DeleteKey("VolVal");
    // }
    void LoadValues()
    {
        float volumeVal = PlayerPrefs.GetFloat("VolVal");
        volSlider.value = volumeVal;
        AudioListener.volume = volumeVal;
    }
}
