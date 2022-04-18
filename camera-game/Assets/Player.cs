using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance = this; // global player instance
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Player Instance;
}
