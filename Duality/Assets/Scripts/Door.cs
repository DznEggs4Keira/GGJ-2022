using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public override string GetDescription() {
        return "Hold E to open the door";
    }

    public override void Interact() {
        Debug.Log("Door Opened");
    }
}
