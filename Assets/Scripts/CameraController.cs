using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Rendering;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour {
    public float horizSens = 100f;
    public float vertSens = 100f;
    public Transform playerBody;
    private float xRot = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * horizSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * vertSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}