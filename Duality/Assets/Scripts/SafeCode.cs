using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCode : Interactable
{
    public override string GetDescription() {
        return "Press E to Enter Code";
    }

    public override void Interact() {
        // show numpad to enter code
    }
}
