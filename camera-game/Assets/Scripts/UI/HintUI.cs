using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    float fOriginalTimer = 15f;
    float fCurrTimer = 1f;
    float fSpeed = 10f;
    float fMax = 255f;
    float fCurr = 0.0f;

    bool bEnabled = false;
    public TextMeshProUGUI myText;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bEnabled)
        {
            fCurrTimer -= Time.deltaTime;
            LerpHint(bEnabled);
        }

        if (fCurrTimer <= 0)
        {
            DisableHint();
        }

        fCurr = Mathf.Clamp(fCurr, 0, fMax);
        fCurrTimer = Mathf.Clamp(fCurrTimer, 0, fOriginalTimer);

        float t = fCurr / fMax;
    }

    void DisableHint()
    {
        bEnabled = false;
        LerpHint(bEnabled);
    }

    void EnableHint()
    {
        bEnabled = true;
        fCurrTimer = fOriginalTimer;
    }

    void LerpHint(bool bStatus)
    {
        if(bStatus)
        {
            fCurr += fSpeed * Time.deltaTime;
        }
        else
        {
            fCurr -= fSpeed * Time.deltaTime;
        }
    }
}
