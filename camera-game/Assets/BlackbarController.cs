using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackbarController : MonoBehaviour
{
    public Blackbar[] blackbars;
    public float[] initialRotations;
    public Vector3 originOffset = new Vector3(0,0,0); // offset from the camera position
    public Vector3 origin {
        get {
            return _camera.transform.position + originOffset;
        }
    }
    private Camera _camera;
    public float rotation = 90f;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
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
