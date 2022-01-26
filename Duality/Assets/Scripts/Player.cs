using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterMovement controller;

    // Torchlight
    [SerializeField] Transform playerArm;
    [SerializeField] Transform Torchlight;

    bool torch = false;

    // Update is called once per frame
    void Update()
    {
        HandleTorch();
    }

    private void HandleTorch() {

        // Flashlight
        if (Input.GetButtonDown("Torchlight")) {
            torch = true;

            //if torch is on, we gotta enable the transform first
            playerArm.gameObject.SetActive(torch);

        } else if (Input.GetButtonUp("Torchlight")) {
            torch = false;

            playerArm.gameObject.SetActive(torch);
        }

        if(playerArm.gameObject.activeInHierarchy) {
            GetComponentInChildren<Light2D>().enabled = torch;
            controller.LookAtMouse(playerArm, Torchlight, torch);
        }
    }

}
