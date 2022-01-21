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
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;

        // Jump
        //if(vertical > 0) {
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

    }

    private void FixedUpdate() {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        //crouch = false;
    }
}
