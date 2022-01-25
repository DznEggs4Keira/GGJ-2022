using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Interactable
{
    public override string GetDescription() {
        Debug.Log($"Talk to Grandma");
        return null;
    }
    public override void Interact() {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
