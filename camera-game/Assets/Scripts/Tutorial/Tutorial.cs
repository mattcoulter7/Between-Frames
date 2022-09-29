using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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
    }

    private void OnEnable()
    {
        myInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        myInput.SwitchCurrentActionMap("UI");
        myInput.actions["Submit"].performed += ctx => OnSubmit();
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

            if (gameObject != null)
            {
                Debug.Log("Null check passed");
                bDestroyed = true;
                this.gameObject.SetActive(false);
                
                //if (gameObject.transform.GetChild(0).gameObject != null)
                //{
                //    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                //    
                //}
            }

        }
    }   
}
