using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceInactive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetActive", 0.2f);
        Invoke("SetInactive", 0.2f);
    }

    // Update is called once per frame

    void SetInactive()
    {
        gameObject.SetActive(false);
    }

    void SetActive()
    {
        gameObject.SetActive(true);
    }

}
