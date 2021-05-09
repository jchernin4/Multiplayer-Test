using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Rendering;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Mirror;
using UnityEditor;

public class PlayerLook : MonoBehaviour {
    public PlayerMovement movementScript;

    public float horizSens = 100f;
    public float vertSens = 100f;
    public Transform playerBody;
    public Transform playerEyes;
    private float xRot = 0f;

    void Start() {
        if (!movementScript.isLocalPlayer || !movementScript.hasAuthority) {
            gameObject.SetActive(false);
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update() {
        if (!movementScript.isLocalPlayer || !movementScript.hasAuthority) {
            gameObject.SetActive(false);
            return;
        }
        float mouseX = Input.GetAxis("Mouse X") * horizSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * vertSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        
        // TODO: This won't be sent over the network
        playerEyes.localRotation = Quaternion.Euler(xRot, 0f, 0f); // Connected to an "Eyes" cube, rotate the entire cube instead of just the camera
        // transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}