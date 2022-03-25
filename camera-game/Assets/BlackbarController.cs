using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackbarController : MonoBehaviour
{
    public Blackbar[] blackbars;
    public float[] initialRotations;
    public float rotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        blackbars = GetComponentsInChildren<Blackbar>();
        initialRotations = new float[blackbars.Length];
        for (int i = 0; i < blackbars.Length; i++){
            initialRotations[i] = blackbars[i].rotation;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < blackbars.Length; i++){
            blackbars[i].rotation = initialRotations[i] + rotation;
        }
    }
}
