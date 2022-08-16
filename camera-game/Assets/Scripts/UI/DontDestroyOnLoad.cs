using UnityEngine;

//<summary>This class was an attempt during testing to force an object to not destroy when switching scenes.
//It has since been implemented directly in the AM code</summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    //<summary>This is the instance of the gameObject this script is attached to</summary>
    static GameObject Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
