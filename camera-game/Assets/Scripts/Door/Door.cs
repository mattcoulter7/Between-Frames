using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// This class should be attached to the door at the end of a level which leads the player to the next level
/// It loads the new scene based on targetSceneName after a duration of waitTime seconds
/// </summary>
public class Door : MonoBehaviour
{
    /// <summary>
    /// What else happens when the user enters the door
    /// </summary>
    public UnityEvent onDoorEnter;

    /// <summary>
    /// What is the name of the scene they are taken to when they enter the door
    /// </summary>
    public string targetSceneName;

    /// <summary>
    /// How long between entering the door and the scene starting to load (in seconds)
    /// </summary>
    public float waitTime;

    /// <summary>
    /// Called from the interact method which trigger the UnityEvent and scene load 
    /// </summary>
    public void EnterDoor()
    {
        onDoorEnter.Invoke();
        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);
    }
}
