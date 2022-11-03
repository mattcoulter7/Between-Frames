using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseHint : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public string myN;
    public int index;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myN = SceneManager.GetActiveScene().name;

        index = SceneManager.GetActiveScene().buildIndex;
        
        switch (myN)
        {
            case "TutLevel1":
                myText.text = "HINT: Press left/right triggers or use mouse scrollwheel to adjust the frame zoom";
                break;
            case "TutLevel2":
                myText.text = "HINT: Move right thumbstick or hold left Click to move the frame";
                break;
            case "TutLevel3":
                myText.text = "HINT: Press Select or Press R to rewind";
                break;
            case "TutLevel4":
                myText.text = "HINT: Press (X)or press F to use flash";
                break;
            case "TutLevel5":
                myText.text = "HINT: Press left/right bumpers or hold right click to rotate the frame";
                break;
            case "TutLevel6":
                myText.text = "TODO";
                break;
            default:
                myText.text = "No Hint";
                break;
        }
    }
}
