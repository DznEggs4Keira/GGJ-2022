using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] SpriteRenderer OverlaySR;

    public override string GetDescription() {
        return "Hold E to open the door";
    }

    public override void Interact() {
        //Fade the Overlay
        //Fade the Door
        //Disable Door Trigger
        //Enable Room
        //Enable Exit Trigger

        StartCoroutine(FadeOverlay());
    }

    IEnumerator FadeOverlay() {
        while (OverlaySR.color.a != 0f) {
            OverlaySR.color = new Color(OverlaySR.color.r, OverlaySR.color.g, OverlaySR.color.b, OverlaySR.color.a - Time.deltaTime);
        }
        yield return null;
    }
}
