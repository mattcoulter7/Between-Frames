using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    public float fHintOriginalTimer = 30f;
    public float fHintCurrTimer = 0f;

    public float fDisplayOriginalTimer = 6f;
    public float fDisplayCurrTimer = 0f;

    private float fBoxOriginalTimer = 6f;
    public float fBoxCurrTimer = 0f;

    private float fSpeed = 100f;
    private float fMax = 255f;
    private float fCurr = 255.0f;
    private float fMax2 = 255f;
    public float fCurr2 = 255.0f;
    private Color temp;
    private Color temp2;

    private bool bEnabled = false;
    public bool bBoxHint = false;
    private float p;
    private float t;

    private float p2;
    private float t2;

    public TextMeshProUGUI myText;
    public GameObject myImg;

    public GameObject BoxHint;
    public TextMeshProUGUI boxHintText;
    public GameObject boxHintImg;

    private void Start()
    {
        temp = new Color(255, 255, 255, 0);
        temp2 = new Color(255, 255, 255, 0);
        fHintCurrTimer = fHintOriginalTimer;
        fDisplayCurrTimer = fDisplayOriginalTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(bEnabled)
        {
            fDisplayCurrTimer -= Time.deltaTime;
        }
        else
        {
            fHintCurrTimer -= Time.deltaTime;
        }

        if(fHintCurrTimer <= 0 && !bEnabled)
        {
            EnableHint();
        }
        if (fDisplayCurrTimer <= 0 && bEnabled)
        {
            DisableHint();
        }
        
        LerpHint(bEnabled);
        fCurr = Mathf.Clamp(fCurr, 0, fMax);
        fDisplayCurrTimer = Mathf.Clamp(fDisplayCurrTimer, 0, fDisplayOriginalTimer);

        t = fCurr / fMax;
        p = 1 - t;
        
        temp.a = p;
        myImg.GetComponent<Image>().color = temp;
        myText.color = temp;


        t2 = fCurr2 / fMax2;
        p2 = 1 - t2;
        temp2.a = p2;

        boxHintText.color = temp2;
        boxHintImg.GetComponent<Image>().color = temp2;

        if (bBoxHint)
        {
            fBoxCurrTimer -= Time.deltaTime;
            
        }

        if (fBoxCurrTimer <= 0 && bBoxHint)
        {
            bBoxHint = false;

        }

        LerpBox(bBoxHint);
        fCurr2 = Mathf.Clamp(fCurr2, 0, fMax2);
        fBoxCurrTimer = Mathf.Clamp(fBoxCurrTimer, 0, fBoxOriginalTimer);
    }

    void DisableHint()
    {
        bEnabled = false;
        fHintCurrTimer = fHintOriginalTimer;
    }

    void EnableHint()
    {
        bEnabled = true;
        fDisplayCurrTimer = fDisplayOriginalTimer;
    }

    public void EnableBoxHint()
    {
        bBoxHint = true;
        fBoxCurrTimer = fBoxOriginalTimer;
    }

    void LerpHint(bool bStatus)
    {
        if(!bStatus)
        {
            fCurr += fSpeed * Time.deltaTime;
        }
        else
        {
            fCurr -= fSpeed * Time.deltaTime;
        }
    }

    void LerpBox(bool bStatus)
    {
        if (!bStatus)
        {
            fCurr2 += fSpeed * Time.deltaTime;
        }
        else
        {
            fCurr2 -= fSpeed * Time.deltaTime;
        }
    }
}
