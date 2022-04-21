using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public float doorWaitTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter(Door door)
    {
        StartCoroutine(openScene(door.targetSceneName));
    }

    IEnumerator openScene(string sceneName)
    {
        yield return new WaitForSeconds(doorWaitTime);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
