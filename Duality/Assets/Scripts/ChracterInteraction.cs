using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChracterInteraction : MonoBehaviour
{
    private Interactable m_Interactable;

    public TextMeshProUGUI interactionText;
    public GameObject interactionHoldGO; // the ui parent to disable when not interacting
    public UnityEngine.UI.Image interactionHoldProgress; // the progress bar for hold interaction type

    private bool isInteracting = false;

    private void Update() {

        if (isInteracting && m_Interactable != null) {
            HandleInteraction(m_Interactable);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        m_Interactable = collision.GetComponentInParent<Interactable>();

        EnableInteraction();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isInteracting = false;
        interactionText.text = "";
    }

    public void CheckInTrigger() {
        ContactFilter2D filter = new ContactFilter2D(); filter.NoFilter();
        Collider2D[] results = null;

        // if still in trigger, enable interaction
        _ = Physics2D.OverlapCollider(GameManager.instance.player.transform.GetComponentInChildren<Collider2D>(), filter, results);

        if (results[0].transform == m_Interactable.transform) {
            EnableInteraction();
        }
    }

    private void EnableInteraction() {

        if (m_Interactable != null) {
            isInteracting = true;
            interactionText.text = m_Interactable.GetDescription();

            interactionHoldGO.SetActive(m_Interactable.interactionType == Interactable.Interactions.Hold_Open);
        }
    }

    private void HandleInteraction(Interactable interactable) {
        switch (interactable.interactionType) {
            case Interactable.Interactions.Talk:
                //if you tap the interaction button
                if(Input.GetButtonDown("Interact")) {
                    interactable.Interact();
                    isInteracting = false;
                    interactionText.text = "";
                }
                break;

            case Interactable.Interactions.Pickup:
                //if you tap the interaction button
                if (Input.GetButtonDown("Interact")) {
                    interactable.Interact();
                    isInteracting = false;
                }
                break;

            case Interactable.Interactions.Hold_Open:
                // if you keep the interaction button pressed
                if (Input.GetButton("Interact")) {
                    // we are holding the key, increase the timer until we reach 1f
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f) {
                        interactable.Interact();
                        isInteracting = false;
                        interactable.ResetHoldTime();
                    }
                } else {
                    interactable.ResetHoldTime();
                }
                interactionHoldProgress.fillAmount = interactable.GetHoldTime();
                break;

            case Interactable.Interactions.Minigame:
                // Safe Code UI
                //if you tap the interaction button
                if (Input.GetButtonDown("Interact")) {
                    interactable.Interact();
                    isInteracting = false;
                }
                break;

            default:
                throw new System.Exception("Unsupported Type of Interactable");
        }
    }
}
