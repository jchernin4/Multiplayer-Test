using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerLook : MonoBehaviour {
    public PlayerNetworkController playerNetworkController;

    public float horizSens = 100f;
    public float vertSens = 100f;
    public Transform playerBody;
    public Transform playerHead;
    private float xRot = 0f;

    void Start() {
        if (!playerNetworkController.isLocalPlayer || !playerNetworkController.hasAuthority) {
            gameObject.SetActive(false);
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update() {
        if (!playerNetworkController.isLocalPlayer || !playerNetworkController.hasAuthority) {
            gameObject.SetActive(false);
            return;
        }
        float mouseX = Input.GetAxis("Mouse X") * horizSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * vertSens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        
        // TODO: This won't be sent over the network
        playerHead.localRotation = Quaternion.Euler(xRot, 0f, 0f); // Connected to a "head" cube, rotate the entire cube instead of just the camera
        // transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}