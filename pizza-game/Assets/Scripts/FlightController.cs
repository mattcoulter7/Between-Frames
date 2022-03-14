using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    public float maxForce = 2f;
    public float yRotationSpeed = 3f;
    public float[] speedModes = new float[4]{50f,100f,150f,200f};
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void HandleSpeedMode(){
        bool[] selectedSpeedModes = new bool[4]{
            Input.GetKey(KeyCode.Alpha1),
            Input.GetKey(KeyCode.Alpha2),
            Input.GetKey(KeyCode.Alpha3),
            Input.GetKey(KeyCode.Alpha4)
        };

        for (int i = 0; i < selectedSpeedModes.Length; i++){
            if (selectedSpeedModes[i]){
                maxForce = speedModes[i];
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // handle forces
        bool forward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool backward = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool up = Input.GetKey(KeyCode.Space);
        bool down = Input.GetKey(KeyCode.LeftShift);

        Vector3 intendedForce = new Vector3(
            (right || left) ? (right ? 1f : -1f) : 0f,
            (up || down) ? (up ? 1f : -1f) : 0f,
            (forward || backward) ? (forward ? 1f : -1f) : 0f
        );
        intendedForce.Normalize();

        Vector3 relativeForce = transform.TransformDirection(intendedForce);
        relativeForce.y = intendedForce.y; // dont want horizontal angle to affect direction
        
        Vector3 truncatedForce = relativeForce.normalized * this.maxForce;

        rb.AddForce(truncatedForce);

        // handle rotations
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * yRotationSpeed, 0));

        // handle speed modes
        HandleSpeedMode();

    }
}
