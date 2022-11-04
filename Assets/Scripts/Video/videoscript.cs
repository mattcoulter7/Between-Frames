using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoscript : MonoBehaviour
{
    VideoPlayer video;
    
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
        //AudioSource source = AM.Instance.GetSFX("CutsceneAudio").source;
        //video.SetTargetAudioSource(0, source);
        // Audio.Invoke();//if (video.isPlaying    
    }
  
    void CheckOver(VideoPlayer vp)
    {
        SceneManager.LoadScene(2); //the scene to load after video finishes
    }
}