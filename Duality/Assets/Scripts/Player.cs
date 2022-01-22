using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 40f;

    CharacterMovement controller;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterMovement>();    
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate() {
        // Move our character
        HandleMovement();
    }

    private void HandleMovement() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
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
