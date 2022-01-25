using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactable
{
    public override string GetDescription() {
        return "Hold E to use ladder";
    }

    public override void Interact() {
        Debug.Log("Going up the ladder");
    }
}
