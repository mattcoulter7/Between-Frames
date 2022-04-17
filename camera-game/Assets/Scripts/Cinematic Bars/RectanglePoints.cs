using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectanglePoints : MonoBehaviour
{
    public float height {
        get
        {
            return transform.localScale.y;
        }
    }
    public float width
    {
        get
        {
            return transform.localScale.x;
        }
    }
    public float depth
    {
        get
        {
            return transform.localScale.z;
        }
    }
    // todo add other points
    public Vector3 bottom
    {
        get
        {
            return transform.position - transform.up * height / 2;
        }
        set
        {
            transform.position = value + transform.up * height / 2;
        }
    }
    public Vector3 left
    {
        get
        {
            return transform.position - transform.right * width / 2;
        }
        set
        {
            transform.position = value + transform.right * width / 2;
        }
    }
    public Vector3 backleft
    {
        get
        {
            return transform.position - transform.right * width / 2 + transform.forward * depth / 2;
        }
        set
        {
            transform.position = value + transform.right * width / 2 - transform.forward * depth / 2;
        }
    }
    public Vector3 frontLeft
    {
        get
        {
            return transform.position - transform.right * width / 2 - transform.forward * depth / 2;
        }
        set
        {
            transform.position = value + transform.right * width / 2 + transform.forward * depth / 2;
        }
    }

    public Vector3 localFrontLeft {
        get {
            return frontLeft - transform.position;
        }
    }

    public Vector3 frontBottomLeft 
    {
        get 
        {
            return frontLeft - transform.up * height / 2;
        }
        set 
        {
            frontLeft = value + transform.up * height / 2;
        }
    }
    public Vector3 frontTopLeft 
    {
        get 
        {
            return frontLeft + transform.up * height / 2;
        }
        set 
        {
            frontLeft = value - transform.up * height / 2;
        }
    }
    public Vector3 frontRight
    {
        get
        {
            return transform.position + transform.right * width / 2 - transform.forward * depth / 2;
        }
        set
        {
            transform.position = value - transform.right * width / 2 + transform.forward * depth / 2;
        }
    }

    public Vector3 frontBottomRight
    {
        get 
        {
            return frontRight - transform.up * height / 2;
        }
        set 
        {
            frontLeft = value + transform.up * height / 2;
        }
    }
    public Vector3 frontTopRight
    {
        get 
        {
            return frontRight + transform.up * height / 2;
        }
        set 
        {
            frontRight = value - transform.up * height / 2;
        }
    }
}
