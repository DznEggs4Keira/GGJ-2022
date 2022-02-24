using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Interactable
{
    [SerializeField] DialogueTrigger[] GrandmaDialogues;

    private bool isInteracting = false;

    public override string GetDescription() {
        return "Press E to talk to grandma";
    }

    public override void Interact() {

        //disable collider
        GetComponentInChildren<Collider2D>().enabled = false;
        
        bool itemRecieved = false;
        
        //if player has item
        var item = GameManager.instance.player.GetComponentInChildren<Item>();
        if (item != null) {
            //take it
            itemRecieved = true;
            GameManager.checkpoint = item.itemId;
            Destroy(item.gameObject);
        }

        GrandmaDialogues[GameManager.checkpoint].TriggerDialogue(itemRecieved);

        //enable collider
        //GetComponentInChildren<Collider2D>().enabled = true;
    }

    public void TriggerDialogueManual() {
        GrandmaDialogues[GameManager.checkpoint].TriggerDialogue(false);
    }
}
