using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour {
    void HandleMovement() {
        if (isLocalPlayer) {
            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horiz * 0.1f, vert * 0.1f, 0);
            transform.position = transform.position + movement;
        }
    }

    void Update() {
        HandleMovement();
    }
}