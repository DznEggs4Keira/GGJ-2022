using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 40f;

    CharacterMovement controller;
    Animator animator;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Start is called before the first frame update
    void Start() 
    {
        controller = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();

        HandleInput();

        // Flashlight
        
        controller.LookAtMouse();
        
    }

    private void FixedUpdate() {
        // Move our character
        HandleMovement();
    }

    private void HandleMovement() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void UpdateAnimation() {
        animator.SetBool("isCrouching", controller.GetCurrentCrouch);

        if (jump) { animator.SetTrigger("isJumping"); }
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
    }
}
