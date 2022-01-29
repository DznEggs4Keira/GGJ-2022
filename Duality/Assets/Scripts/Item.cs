using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {

    [SerializeField] Transform player;
    public int itemId;
    public bool recieved = false;

    public override string GetDescription() {
        return "Press E to pickup ";
    }

    public override void Interact() {
        //parent to player
        transform.SetParent(player.transform);
        transform.localPosition = Vector3.up * 5f;

        //enable a public static bool or int?

    }
}
