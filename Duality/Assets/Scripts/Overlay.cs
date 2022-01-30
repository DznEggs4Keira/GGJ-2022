using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    [SerializeField] SpriteRenderer OverlaySR;
    [SerializeField] GameObject ToDisable;
    [SerializeField] GameObject RoomToUnlock;

    public float fadeSpeed = 1f;
    public float fadeTime = 1f;
    public bool fadeIn = false;

    private void Start() {
        OverlaySR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (fadeIn) {
            float Fade = Mathf.SmoothDamp(0f, 1f, ref fadeSpeed, fadeTime);
            OverlaySR.color = new Color(1f, 1f, 1f, Fade);
        }

        if (!fadeIn) {
            float Fade = Mathf.SmoothDamp(1f, 0f, ref fadeSpeed, fadeTime);
            OverlaySR.color = new Color(1f, 1f, 1f, Fade);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        fadeIn = true;
        if (ToDisable != null) ToDisable.SetActive(false);
        if (RoomToUnlock != null) RoomToUnlock.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        fadeIn = false;
        if (ToDisable != null) ToDisable.SetActive(true);
        if (RoomToUnlock != null) RoomToUnlock.SetActive(false);
    }
}
