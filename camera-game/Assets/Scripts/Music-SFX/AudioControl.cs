using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private Slider BGMSlider = null;
    private float BGMFloat;
    private Sound BGM;

    public GameObject AudioObject;

    AudioSource[] SoundList = null;
    readonly string AudioList = "AudioList";

    //awake called when script is first instantiated in scene
    void Awake()
    {

        Debug.Log("should be first");
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioObject = GameObject.Find(AudioList);

        SoundList = AudioObject.GetComponents<AudioSource>();
        //BGM.loop = true;
        //BGM.source.Play();
        //SoundList[1].Play();
        //PlayAudio(1);

        //if (SoundList[1].isPlaying)
        //{
        //    Debug.Log("Sound playing");
        //}


        //string test = myOne.name;
        Debug.Log("should be second");
            //GetComponent<Sound>();

        //BGM.source.Play();
    }

    void PlayAudio(int sourceNum)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = SoundList[sourceNum].clip;
        audio.Play();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
