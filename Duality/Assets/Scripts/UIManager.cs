using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public void PlayButton() {
        GameManager.instance.setTimescale(1f); ;
    }

    public void ExitButton() {
        Application.Quit();
    }

    public void ShowTooltip(bool value) {

    }
}
