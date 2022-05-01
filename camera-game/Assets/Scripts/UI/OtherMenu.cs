using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherMenu : MonoBehaviour
{
    public GameObject mainMenu;
    // public void OnVolumeSlide(Slider slider){
    // }

    public void OnBack(){
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
