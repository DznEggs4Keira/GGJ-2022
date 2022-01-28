using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Interactable
{
    public override string GetDescription() {
        return "Press E to talk to grandma";
    }

    public override void Interact() {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
