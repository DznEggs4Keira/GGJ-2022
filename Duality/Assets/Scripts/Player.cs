using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 40f;

    [SerializeField] CharacterMovement controller;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool torch = false;

    private void Start() {
        controller = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();

        HandleInput();

        HandleTorch();

    }

    private void FixedUpdate() {
        // Move our character
        HandleMovement();
    }

    private void HandleMovement() {
        //controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void HandleTorch() {
        GetComponentInChildren<Light2D>().enabled = torch;
        //controller.LookAtMouse(torch);
    }

    private void UpdateAnimation() {
        //animator.SetBool("isCrouching", controller.GetCurrentCrouch);

        //if (jump) { animator.SetTrigger("isJumping"); }
    }

    private void HandleInput() {

        // Move
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;

        // Jump
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        // Crouch
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }

        // Flashlight
        if (Input.GetButtonDown("Torchlight")) {
            torch = true;
        } else if (Input.GetButtonUp("Torchlight")) {
            torch = false;
        }
    }
}
