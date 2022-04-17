using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : MonoBehaviour
{
    public Vector3 position;
    public bool[] lockPositionAxis = new bool[3]{false,false,false};
    public Vector3 rotation;
    public bool[] lockRotationAxis = new bool[3]{false,false,false};
    public Vector3 scale;
    public bool[] lockScaleAxis = new bool[3]{false,false,false};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            lockPositionAxis[0] ? position.x : transform.position.x,
            lockPositionAxis[1] ? position.y : transform.position.y,
            lockPositionAxis[2] ? position.z : transform.position.z
        );

        transform.rotation = Quaternion.Euler(
            lockRotationAxis[0] ? rotation.x : transform.rotation.x,
            lockRotationAxis[1] ? rotation.y : transform.rotation.y,
            lockRotationAxis[2] ? rotation.z : transform.rotation.z
        );

        transform.localScale = new Vector3(
            lockScaleAxis[0] ? scale.x : transform.localScale.x,
            lockScaleAxis[1] ? scale.y : transform.localScale.y,
            lockScaleAxis[2] ? scale.z : transform.localScale.z
        );
    }
}
