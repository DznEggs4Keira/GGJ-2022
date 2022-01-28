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

        //StartCoroutine(FadeOverlay());
    }

    IEnumerator FadeOverlay() {
        yield return null;
    }
}
