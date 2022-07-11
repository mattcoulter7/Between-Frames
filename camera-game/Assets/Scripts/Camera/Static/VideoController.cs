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

    // NOTES:
    //Whole canvas is kind of laggy. Could consider leaving it as a quad with video player texture. Also might solve pause render ordedr problem
}
