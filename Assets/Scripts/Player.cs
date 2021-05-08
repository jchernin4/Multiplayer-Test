using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour {
    [SyncVar(hook = nameof(OnHelloCountChanged))]
    int helloCount = 0;
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

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.X)) {
            Debug.Log("Sending hello!");
            Hello();
        }
    }

    [Command]
    void Hello() {
        Debug.Log("Received hello from client");
        helloCount++;
        ReplyHello();
    }

    [TargetRpc]
    void ReplyHello() {
        Debug.Log("Received hello from server");
    }

    void OnHelloCountChanged(int oldCount, int newCount) {
        Debug.Log($"{oldCount} previous hellos, {newCount} hellos now");
    }
}