using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public UnityEvent onDoorEnter;
    public string targetSceneName;
    public float waitTime;

    public void EnterDoor()
    {
        StartCoroutine(openScene(targetSceneName));
        onDoorEnter.Invoke();
    }

    IEnumerator openScene(string sceneName)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
