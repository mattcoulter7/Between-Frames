using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    //void Start()
    //{


    //}

    //// Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            Debug.Log("Video is playing still");
        }
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }
}
