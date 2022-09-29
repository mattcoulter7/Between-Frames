using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    public string defaultState;
    public List<StateMachineLayer> layers = new List<StateMachineLayer> { };
    private List<State> history = new List<State> { null };
    private string selectedLayer = "";
    public State currentState
    {
        get
        {
            if (history.Count == 0) history.Add(null);
            return history[0];
        }
        set
        {
            if (history.Count == 0) history.Add(null);
            history[0] = value;
        }
    }
    private Dictionary<string, State> states = new Dictionary<string, State>();
    public void SelectLayer(string layer)
    {
        selectedLayer = layer;
    }
    public void ChangeState(string state) // overrides the current state
    {
        if (selectedLayer == "")
        {
            State stateObj = state == "" ? null : states[state];
            ChangeState(stateObj);
        } 
        else
        {
            StateMachineLayer layerObj = GetStateMachineLayer(selectedLayer);
            layerObj.ChangeState(state);
            selectedLayer = "";
        }
    }

    public void AddState(string state) // adds to front of history
    {
        if (selectedLayer == "")
        {
            if (currentState != null) currentState.Exit();

            State stateObj = states[state];
            if (currentState == null) currentState = stateObj;
            else history.Insert(0, stateObj);

            currentState.Enter();
        }
        else
        {
            StateMachineLayer layerObj = GetStateMachineLayer(selectedLayer);
            layerObj.ChangeState(state);
            selectedLayer = "";
        }
    }

    public void RemoveState() // removes from of history
    {
        if (selectedLayer == "")
        {
            if (currentState != null)
            {
                currentState.Exit();
                history.RemoveAt(0);

                if (currentState != null) currentState.Enter();
            }
        }
        else
        {
            StateMachineLayer layerObj = GetStateMachineLayer(selectedLayer);
            layerObj.RemoveState();
            selectedLayer = "";
        }
    }

    public void RegisterState(string value, State obj)
    {
        if (selectedLayer == "")
        {
            states[value] = obj;
        }
        else
        {
            StateMachineLayer layerObj = GetStateMachineLayer(selectedLayer);
            layerObj.RegisterState(value,obj);
            selectedLayer = "";
        }
    }
    private void Start()
    {
        if (defaultState != "") ChangeState(defaultState);
        foreach (StateMachineLayer layer in layers)
        {
            layer.Start();
        }
    }


    private void Update()
    {
        if (currentState != null) currentState.HandleInput();
        if (currentState != null) currentState.HandleShouldChangeState();
        if (currentState != null) currentState.LogicUpdate();
        foreach (StateMachineLayer layer in layers)
        {
            layer.Update();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null) currentState.PhysicsUpdate();
        foreach (StateMachineLayer layer in layers)
        {
            layer.FixedUpdate();
        }
    }
    private StateMachineLayer GetStateMachineLayer(string name)
    {
        StateMachineLayer layer = layers.Find(x => x.name == name);
        if (layer == null) throw new Exception("Layer '" + name + "' does not exist on " + gameObject.name);
        return layer;
    }
    private void ChangeState(State stateObj) // overrides the current state
    {
        if (currentState == stateObj) return; // can't re-enter the current state

        if (currentState != null) currentState.Exit();
        currentState = stateObj;
        if (currentState != null) currentState.Enter();
    }
}