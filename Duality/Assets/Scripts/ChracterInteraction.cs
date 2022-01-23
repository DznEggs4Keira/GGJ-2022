using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterInteraction : MonoBehaviour
{
    public float interactionDistance;

    public TMPro.TextMeshProUGUI interactionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;

        bool successfulHit = false;

        if(Physics.Raycast(ray, out hit, interactionDistance)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if(interactable != null) {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                successfulHit = true;
            }
        }

        if (!successfulHit) interactionText.text = "";

    }

    private void HandleInteraction(Interactable interactable) {
        switch (interactable.interactionType) {
            case Interactable.Interactions.Pickup:
                //if you tap the interaction button
                if(Input.GetButtonDown("Interact")) {
                    interactable.Interact();
                }
                break;

            case Interactable.Interactions.Hold:
                // if you keep the interaction button pressed
                if (Input.GetButton("Interact")) {
                    interactable.Interact();
                }
                break;

            case Interactable.Interactions.Minigame:
                // call a minigame function for solving the puzzle?
                break;

            default:
                throw new System.Exception("Unsupported Type of Interactable");
        }
    }
}
