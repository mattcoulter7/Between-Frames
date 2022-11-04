using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This component simply exists to load a scene from an animation callback
/// </summary>
public class SceneHelper : MonoBehaviour
{
    /// <summary>
    /// Reload the current scene
    /// </summary>
    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// Load a scene by a specific name
    /// </summary>
    /// <param name="name">The name of the scene to load</param>
    public void LoadScene(string name){
        SceneManager.LoadScene(name);
    }
}
