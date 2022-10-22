using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanner : MonoBehaviour
{
    public List<Camera> cameraReferences = new List<Camera>(); //ensure there is at least 2 cameras
    public Camera startCamera;
    public float stepSize = 0.01f;
    public bool stopAtEnds = false;
    public float startT = 0;
    private Camera _currentReference;
    private float _t; // t value between two refs

    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _currentReference = startCamera;
        _t = startT;
        _mainCamera = GetComponent<Camera>();
        UpdateCamera();
    }
    private int GetCurrentIndex()
    {
        return cameraReferences.IndexOf(_currentReference);
    }
    private Camera GetNextCam()
    {
        int currIndex = GetCurrentIndex();
        if (currIndex < cameraReferences.Count - 1)
        {
            return cameraReferences[currIndex + 1];
        }
        return cameraReferences[0];
    }
    private Camera GetPreviousCam()
    {
        int currIndex = GetCurrentIndex();
        if (currIndex == 0)
        {
            return cameraReferences[cameraReferences.Count - 1];
        }
        return cameraReferences[currIndex - 1];
    }

    private Vector3 GetPosition()
    {
        Vector3 startPos = _currentReference.transform.position;
        Vector3 endPos = GetNextCam().transform.position;
        Vector3 lerpedPos = Vector3.Lerp(startPos, endPos, _t);
        Debug.Log("T:" + _t);
        Debug.Log(startPos);
        Debug.Log(endPos);
        Debug.Log(lerpedPos);

        return lerpedPos;
    }

    private Quaternion GetRotation()
    {
        Quaternion startPos = _currentReference.transform.rotation;
        Quaternion endPos = GetNextCam().transform.rotation;
        Quaternion lerpedPos = Quaternion.Lerp(startPos, endPos, _t);

        return lerpedPos;
    }

    void UpdateT(float direction)
    {
        _t += direction * stepSize;
    }

    void UpdateCamera()
    {

        // update t with direction
        _mainCamera.transform.position = GetPosition();
        _mainCamera.transform.rotation = GetRotation();

        // ensure camera ref is updated
        if (_t > 1)
        {
            // if current reference is 1 away from the end, don't progress
            if (stopAtEnds && _currentReference == cameraReferences[cameraReferences.Count - 2])
            {
                _t = 1;
            }
            else
            {
                _t = 0;
                _currentReference = GetNextCam();
            }
        }
        else if (_t < 0)
        {
            // don't both going further left is you're already at the end
            if (stopAtEnds && _currentReference == cameraReferences[0])
            {
                _t = 0;
            }
            else
            {
                _t = 1;
                _currentReference = GetPreviousCam();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        if (direction == 0) return; // do nothing when no input detected
        UpdateT(direction);
        UpdateCamera();
    }
}