using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : MonoBehaviour
{
    public Vector3 position;
    public bool[] lockAxis = new bool[3]{false,false,false};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            lockAxis[0] ? position.x : transform.position.x,
            lockAxis[1] ? position.y : transform.position.y,
            lockAxis[2] ? position.z : transform.position.z
        );
    }
}
