using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour {
    public CharacterController controller;
    public Transform groundCheck;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector3 velocity;

    // TODO: Replace groundcheck with raycast, makes it simpler
    public float groundCheckRadius = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    private void Start() {
        if (!isLocalPlayer || !hasAuthority) {
            return;
        }
        controller = GetComponent<CharacterController>();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    
    void Update() {
        if (!isLocalPlayer || !hasAuthority) {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * (speed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}