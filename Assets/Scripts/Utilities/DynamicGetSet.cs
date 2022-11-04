using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGetSet : MonoBehaviour
{
    public DynamicModifier getterSetter;
    void Start()
    {
        getterSetter.OnInitialise();
    }
    // Update is called once per frame
    void Update()
    {
        getterSetter.Invoke();
    }
}
