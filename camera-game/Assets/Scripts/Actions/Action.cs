using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    bool _running = false;

    void Update(){
        if (_running){
            WhenRunning();
        }
    }
    public abstract void WhenRunning();
    public abstract void OnRun();
    public abstract void OnStop();
    public void Stop(){
        _running = false;
        OnStop();
    }
    public void Run()
    {
        _running = true;
        OnRun();
    }

    public void Invoke()
    {
        throw new NotImplementedException();
    }
}
