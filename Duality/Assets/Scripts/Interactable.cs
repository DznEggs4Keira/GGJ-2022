using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum Interactions {
        Pickup,     //Click to pickup and item or talk to someone
        Hold,       //Open Doors and carry objects
        Minigame    // Puzzle Minigame

    }

    public Interactions interactionType;

    public abstract string GetDescription();
    public abstract void Interact();

}
