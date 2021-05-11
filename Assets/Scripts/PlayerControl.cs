using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public PlayerNetworkController playerNetworkController;
    public float shotCooldown = 0.5f;

    public float currentCooldown = 0f;
    
    void Update() {
        if (!playerNetworkController.isLocalPlayer || !playerNetworkController.hasAuthority) {
            return;
        }
        
        if (Input.GetMouseButton(0) && currentCooldown <= 0f) {
            playerNetworkController.CmdSpawnBullet();

            currentCooldown = shotCooldown;
        }

        if (currentCooldown > 0f) {
            currentCooldown -= Time.deltaTime;
        }
    }

}