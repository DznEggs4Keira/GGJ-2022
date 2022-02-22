using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool disablePlayer = false;

    public CharacterMovement controller;
    [SerializeField] private ChracterInteraction interactor;

    // Torchlight
    [SerializeField] Transform playerArm;

    bool torch = false;

    private void Start() {
        controller = GetComponent<CharacterMovement>();
        interactor = GetComponent<ChracterInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if(disablePlayer) {
            controller.enabled = !disablePlayer;
            interactor.enabled = !disablePlayer;
            return;
        }

        HandleTorch();
    }

    private void HandleTorch() {

        // if grandma has given torch
        if(GameManager.checkpoint >= 1) {
            // Flashlight
            if (Input.GetButtonDown("Torchlight")) {
                torch = true;

                //if torch is on, we gotta enable the transform first
                playerArm.gameObject.SetActive(torch);

            } else if (Input.GetButtonUp("Torchlight")) {
                torch = false;

                playerArm.gameObject.SetActive(torch);
            }

            if (torch) {
                controller.LookAtMouse(playerArm);
            }
        }
    }

}
