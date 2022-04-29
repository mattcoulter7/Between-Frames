using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVol : MonoBehaviour
{
    //public AudioMixer mixer;

    public void SetLevel(float slideVal)
    {
        //mixer.SetFloat( "FXVol", Mathf.Log10 (slideVal) * 20 );
        // PlayerPrefs.SetFloat("Vol", mixer.GetFloat("FXVol", out slideVal));
        AudioListener.volume = slideVal;//Mathf.Log10 (slideVal) * 20;
    }


}
