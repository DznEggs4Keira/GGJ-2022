using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Interactable
{
    public static int checkpoint = 0;
    public static bool itemRecieved = false;

    [SerializeField] DialogueTrigger[] GrandmaDialogues;
    [SerializeField] Player Player;

    public override string GetDescription() {
        return "Press E to talk to grandma";
    }

    public override void Interact() {
        //if player has item
        var item = Player.GetComponentInChildren<Item>();
        if (item != null) {
            //take it
            itemRecieved = true;
            checkpoint = item.itemId;
            Destroy(item.gameObject);
        }

        GrandmaDialogues[checkpoint].TriggerDialogue();
    }
}
