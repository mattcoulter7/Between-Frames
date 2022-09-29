using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class Tutorial : MonoBehaviour
{
    PlayerInput myInput;
    bool bDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        myInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
    }

    void Awake()
    {
        myInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        myInput.SwitchCurrentActionMap("UI");
        myInput.actions["Submit"].performed += ctx => OnSubmit();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSubmit()
    {
        try
        {
            if (bDestroyed != true)
            {
                Debug.Log("Destroy check passed");
                if (myInput == null)
                {
                    myInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
                }
                myInput.SwitchCurrentActionMap("Player");
                myInput.actions["Submit"].performed -= ctx => OnSubmit();

                Debug.Log("input check passed");
                if (bDestroyed)
                {
                    return;
                }
                else if (this != null)
                {
                    Debug.Log("Null check passed");
                    bDestroyed = true;
                    gameObject.SetActive(false);
                    //Destroy(this);

                    //if (gameObject.transform.GetChild(0).gameObject != null)
                    //{
                    //    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    //    
                    //}
                }

            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }   
}
