using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTest : MonoBehaviour
{


    public void DestroyAM()
    {
        AM.Instance.KillYourself();
        //Destroy(AM.Instance);
        Debug.Log("DESTROYED");
    }
}
