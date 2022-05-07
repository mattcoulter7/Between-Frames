using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("isWalking", true);

        }
        else
        {
            anim.SetBool("isWalking", false);
        }*/

        anim.SetBool("isWalking", Input.GetButton("Horizontal"));
    }

    public void EnableState(string name){
        anim.SetBool(name, true);
    }

    public void DisableState(string name){
        anim.SetBool(name, false);
    }
}
