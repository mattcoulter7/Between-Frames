using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackbar : MonoBehaviour
{
    public float yAnchor = 0f;
    // Start is called before the first frame update
    public Vector3 left {
        get {
            return new Vector3(0,0,0);
        }
    }
    public Vector3 right {
        get {
            return new Vector3(0,0,0);
        }
    }
    void Start()
    {
        // ensure units are relative to size of the object
        //anchors[i].localPos.x *= transform.localScale.x / 2;
        //anchors[i].localPos.y *= transform.localScale.y / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}

