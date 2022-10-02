using UnityEngine;

/// <summary>This class is responsible for calling the PlayBGM function from AM</summary>
public class PlayBGM : MonoBehaviour
{
    /// <summary>This method calls the play BGM function from AM and tells it which Sound to play</summary>
    /// <param name="name">This is a string that has the name of the Sound to play</param>
    public void Play(string name)
    {
        AM.Instance.PlayBGM(name);
    }

    public void Stop(string name)
    {
        AM.Instance.StopBGM(name);
    }

}
