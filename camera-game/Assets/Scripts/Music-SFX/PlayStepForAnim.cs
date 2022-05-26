using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStepForAnim : MonoBehaviour
{
    public GameObject SoundEvent;

    private void Awake()
    {
        SoundEvent = GameObject.Find("SoundEvent");
    }

    public void Step()
    {
        SoundEvent.GetComponent<PlaySound>().Play("HouseFootsteps");
    }

}
