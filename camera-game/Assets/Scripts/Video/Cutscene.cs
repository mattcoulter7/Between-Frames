using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    public string nextScene;
    private VideoPlayer video;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
    }

    private void CheckOver(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene); //the scene to load after video finishes
    }
}