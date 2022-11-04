using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public AudioSource soundPlayer;
    //public AudioSource[] soundtracksArray;
    public ArrayList soundtracksArray;
    GameObject[] gameSound;


    void Awake()
    {
        gameSound = GameObject.FindGameObjectsWithTag("Sound");
        Populate(); 
    }
    void Update()
    {
        if (!soundPlayer.isPlaying)
        {
            PlayAudioTracksRandom();
        }
    }


    void PlayAudioTracksRandom()
    {
        if (!soundPlayer.isPlaying)
        {
            //soundPlayer.clip = soundtracksArray[1].GetComponent;
            soundPlayer.Play();
            //soundtracks.PlayDelayed(44100);
        }
    }

    void Populate()
    { 
        for (int i = 0; i < gameSound.Length; i++)
        {
            soundtracksArray[i] = gameSound[i].GetComponent<AudioSource>();
        }

    }
}
