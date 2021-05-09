using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Mirror;
using UnityEngine;

[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class OLDPlayerController : NetworkBehaviour {
    public float walkSpeed = 4f;
    [SyncVar(hook = nameof(OnHelloCountChanged))]
    int helloCount = 0;

    void Update() {
        if (isLocalPlayer) {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            // ReSharper disable once Unity.InefficientPropertyAccess
            Vector3 movement = transform.right * x + transform.forward * z;
            CharacterController controller = GetComponent<CharacterController>();

            controller.Move(movement * (walkSpeed * Time.deltaTime));
            
            if (Input.GetKeyDown(KeyCode.X)) {
                Debug.Log("Sending hello!");
                Hello();
            }
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