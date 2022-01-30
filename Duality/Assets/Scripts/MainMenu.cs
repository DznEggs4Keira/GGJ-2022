using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    public void PlayButton() {
        Time.timeScale = 1f;
    }

    public void ExitButton() {
        Application.Quit();
    }
}
