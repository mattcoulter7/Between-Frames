using UnityEngine;
using UnityEngine.Video;

/// <summary>This class controls the video that plays during the character rewind state.
/// This is because the video plays a static screen which is reminiscent of VHS rewind static, and is an image on a canvas that holds a video</summary>
public class VideoController : MonoBehaviour
{
    /// <summary>This is the video player located in the image within the canvas
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

    /// <summary>This method calls the pause function in the video player</summary>
    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    /// <summary>This method calls the play function in the video player</summary>
    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    // NOTES:
    //Whole canvas is kind of laggy. Could consider leaving it as a quad with video player texture. Also might solve pause render order problem
}
