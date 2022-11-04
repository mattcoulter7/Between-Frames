using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HintText : MonoBehaviour
{
    public TextMeshProUGUI myText;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name)
        {
            case "TutLevel1":
                myText.text = "Hint: Try using frame zoom";
                break;
            case "TutLevel2":
                myText.text = "Hint: Try moving the frame";
                break;
            case "TutLevel3":
                myText.text = "Hint: Try pushing the box";
                break;
            case "TutLevel4":
                myText.text = "Hint: Try using flash on the light source";
                break;
            case "TutLevel5":
                myText.text = "Hint: Try tilting the frame";
                break;
            case "TutLevel6":
                myText.text = "Hint: TODO";
                break;
            default:
                myText.text = "No Hint";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
