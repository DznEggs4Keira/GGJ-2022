using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    Animator fader;

    // Start is called before the first frame update
    void Awake()
    {
        fader = GetComponent<Animator>();
    }

    public void Fade() {
        fader.SetTrigger("FadeOut");
    }

    public void EndGameFade() {
        GameManager.instance.ReloadScene();
    }

    public void StartGameFade() {
        GameManager.instance.setTimescale(0f);
    }

}
