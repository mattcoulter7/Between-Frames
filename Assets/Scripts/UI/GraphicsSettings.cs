using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicsSettings : MonoBehaviour
{
    //public Toggle fullscreenTog in the future perhaps
    public Toggle vsyncTog;

    public List<Resolution> resolutions = new();

    public TMP_Text resolutionLabel;

    private string ResPref = "SelectedRes";
    private int selectedRes;

    public static readonly string FirstScreen = "FirstScreen";
    private int firstScreen;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("selected res: " + selectedRes);
        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }
        firstScreen = PlayerPrefs.GetInt(FirstScreen);

        if (firstScreen == 0)
        {
            InitialRes();

            PlayerPrefs.SetInt(FirstScreen, -1);
        }
        else
        {
            selectedRes = PlayerPrefs.GetInt(ResPref);
        }
        UpdateResLabel();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResLeft()
    {

        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = 0;
        }

        UpdateResLabel();
    }

    public void ResRight()
    {

        selectedRes++;
        if (selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count -1;
        }

        UpdateResLabel();

    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedRes].horizontal.ToString() + " x " + resolutions[selectedRes].vertical.ToString();
    }

    private void InitialRes()
    {
        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {

                foundRes = true;
                selectedRes = i;

                UpdateResLabel();

            }

        }

        if (!foundRes)
        {
            Resolution newRes = new();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            //resolutions.Insert();
            //Add(newRes);
            selectedRes = resolutions.Count - 1;
            UpdateResLabel();


        }
    }
    public void ApplyGraphics()
    {
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, Screen.fullScreen);

        PlayerPrefs.SetInt(ResPref, selectedRes);
    }

}
