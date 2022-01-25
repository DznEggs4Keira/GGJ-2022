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

        Debug.Log("Talking to Grandma");
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
