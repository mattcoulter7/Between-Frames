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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
