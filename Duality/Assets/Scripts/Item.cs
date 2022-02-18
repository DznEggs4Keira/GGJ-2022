using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {

    [SerializeField] Transform player;
    public int itemId;

    public override string GetDescription() {
        return "Press E to pickup ";
    }

    public override void Interact() {
        //check if at the right checkpoint to interact with item
        if (Grandma.checkpoint == itemId - 1) {
            // Allow interaction

            //parent to player
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up * 5f;
        }
    }
}
