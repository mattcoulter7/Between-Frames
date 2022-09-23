using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    PlayerInput myInput;


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
        myInput.actions["Submit"].performed -= ctx => OnSubmit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSubmit()
    {
        myInput.SwitchCurrentActionMap("Player");
        this.gameObject.SetActive(false);
    }
}
