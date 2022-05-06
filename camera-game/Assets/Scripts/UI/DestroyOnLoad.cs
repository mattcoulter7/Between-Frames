using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public static AM instance;
    void Awake()
    {
        //Debug.Log("Awake on Destroy happening first");
        //if (AM.Instance != null)
        //{
        //    Debug.Log("GO is: " + gameObject);
        //    Destroy(this.gameObject);

        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}

        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<AM>();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);



    }
    void Start()
    {
        //Debug.Log("Start on Destroy Happening first");
        //if (instance != null)
        //{
        //    Debug.Log("GO is: " + gameObject);
        //    Destroy(gameObject);

        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
