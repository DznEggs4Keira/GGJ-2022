using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Interactable
{
    public Transform m_SpeechBubble;

    public override string GetDescription() {
        return "Press E to talk to grandma";
    }
    public override void Interact() {
        EnableSpeechBubble();
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    private void EnableSpeechBubble() {
        m_SpeechBubble.gameObject.SetActive(true);
    }
}
