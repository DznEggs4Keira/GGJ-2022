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

    [HideInInspector] public FadeManager fadeManager;
    [HideInInspector] public UIManager uiManager;
    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public DialogueManager dialogueManager;
    [HideInInspector] public Player player;

    public static int checkpoint = 0;

    private void Start() {
        // we can set the Managers
        fadeManager = FindObjectOfType<FadeManager>();
        uiManager = FindObjectOfType<UIManager>();
        audioManager = FindObjectOfType<AudioManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        player = FindObjectOfType<Player>();
    }

    public void ReloadScene() {

        //reset checkpoint
        checkpoint = 0;

        //reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //Application.Quit();
    }

    public void setTimescale(float value) {
        Time.timeScale = value;
    }
}
