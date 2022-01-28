using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] SpriteRenderer OverlaySR;
    [SerializeField] Collider2D OverlayTrigger;
    [SerializeField] Sprite doorClosed;
    [SerializeField] Sprite doorOpened;

    public float fadeSpeed = 1f;
    public float fadeTime = 1f;
    public bool fadeIn = true;

    public override string GetDescription() {
        return "Hold E to open the door";
    }

    public override void Interact() {
        //Disable Door Trigger
        GetComponent<Collider2D>().enabled = false;

        //Enable Room


        //Fade the Overlay
        StartCoroutine(FadeOverlay());

        //Switch the sprite for the overlay
        OverlaySR.sprite = doorOpened;

        //Enable Exit Trigger
        OverlayTrigger.enabled = true;
    }

    IEnumerator FadeOverlay() {
        float Fade = Mathf.SmoothDamp(0f, 1f, ref fadeSpeed, fadeTime);
        OverlaySR.color = new Color(1f, 1f, 1f, Fade);

        yield return null;
    }
}
