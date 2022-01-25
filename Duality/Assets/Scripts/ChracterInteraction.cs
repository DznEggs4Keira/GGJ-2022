using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChracterInteraction : MonoBehaviour
{
    private Interactable m_Interactable;

    public TextMeshProUGUI interactionText;

    private bool isInteracting = false;

    private void Update() {

        if (isInteracting && m_Interactable != null) {
            HandleInteraction(m_Interactable);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        m_Interactable = collision.GetComponentInParent<Interactable>();

        if (m_Interactable != null) {
            isInteracting = true;
            interactionText.text = m_Interactable.GetDescription();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isInteracting = false;
        interactionText.text = "";
    }

    private void HandleInteraction(Interactable interactable) {
        switch (interactable.interactionType) {
            case Interactable.Interactions.Pickup:
                //if you tap the interaction button
                if(Input.GetButtonDown("Interact")) {
                    interactable.Interact();
                    isInteracting = false;
                }
                break;

            case Interactable.Interactions.Hold:
                // if you keep the interaction button pressed
                if (Input.GetButton("Interact")) {
                    interactable.Interact();
                    isInteracting = false;
                }
                break;

            case Interactable.Interactions.Minigame:
                // call a minigame function for solving the puzzle?
                isInteracting = false;
                break;

            default:
                throw new System.Exception("Unsupported Type of Interactable");
        }
    }
}
