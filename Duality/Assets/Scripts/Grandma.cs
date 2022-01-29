using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Interactable
{
    public static int checkpoint = 0;

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
            item.recieved = true;
            itemRecieved = item.recieved;
            checkpoint = item.itemId;
            Destroy(item.gameObject);
        }

        GrandmaDialogues[checkpoint].TriggerDialogue(itemRecieved);
    }
}
