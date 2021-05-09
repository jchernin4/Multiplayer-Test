using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLDCameraController : MonoBehaviour {
    public float horizSens = 100f;
    public float vertSens = 100f;

    private float xRot = 0f;
    public Transform player;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        //transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private void Update() {
        float mouseX = Input.GetAxis("Mouse X") * horizSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * vertSens * Time.deltaTime; // Test

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;
    }
}