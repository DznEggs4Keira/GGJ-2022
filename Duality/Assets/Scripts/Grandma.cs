using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Interactable
{
    [SerializeField] DialogueTrigger[] GrandmaDialogues;
    [SerializeField] Player Player;

    public override string GetDescription() {
        return "Press E to talk to grandma";
    }

    public override void Interact() {
        bool itemRecieved = false;
        //if player has item
        var item = Player.GetComponentInChildren<Item>();
        if (item != null) {
            //take it
            itemRecieved = true;
            GameManager.checkpoint = item.itemId;
            Destroy(item.gameObject);
        }

        GrandmaDialogues[GameManager.checkpoint].TriggerDialogue(itemRecieved);
    }

    public void TriggerDialogueManual() {
        GrandmaDialogues[GameManager.checkpoint].TriggerDialogue(false);
    }
}
