using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will sequence any tracks to play in order or whatnot.
/// </summary>
public class BGMSequencer : MonoBehaviour
{
    //public Sequence[] sequence;
    public string[] songsToAdd;
    public List<Sound> sequence;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(string s in songsToAdd)
        {
            sequence.Add(AM.Instance.GetBGM(s));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart()
    {
        //gonna force it just coz PAX is coming
        sequence[0].source.Play();
        sequence[1].source.PlayDelayed(sequence[0].source.clip.length);
        
    }
}


//public class Sequence : MonoBehaviour
//{
//    public Sound track;
//    public string trackName;
//    public float playAs;
//    public bool loop;
//    public int loopTime;
//    public bool loopIndefinite;
//    public bool done;

//if (!sequence[i - 1].track.source.isPlaying
//    && sequence[i - 1].done
//    && (i - 1) > 0
//    &&)
//{
//    sequence[i].track.source.Play();
//}


//}