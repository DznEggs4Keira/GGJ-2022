using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    #endregion

    public FadeManager fadeManager;

    public void ReloadScene() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Application.Quit();
    }

    public void setTimescale(float value) {
        Time.timeScale = value;
    }
}
