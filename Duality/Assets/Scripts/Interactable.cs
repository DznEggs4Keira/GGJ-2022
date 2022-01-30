using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum Interactions {
        Talk,   // Talk to someone
        Pickup, //Click to pickup and item
        Hold_Open, //Open Doors
        Minigame  // Puzzle Minigame
    }

    float holdTime;

    public Interactions interactionType;

    public abstract string GetDescription();
    public abstract void Interact();

    public void IncreaseHoldTime() => holdTime += Time.deltaTime;
    public void ResetHoldTime() => holdTime = 0f;
    public float GetHoldTime() => holdTime;

}
