using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {

    public override string GetDescription() {
        return "Press E to pickup ";
    }

    public override void Interact() {
        //parent to player
        //enable a public static bool or int?

        Debug.Log("Item picked");

    }
}
