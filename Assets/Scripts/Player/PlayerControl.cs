using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerControl : NetworkBehaviour {
    public GameObject bulletPrefab;
    public GameObject bulletExit;
    public float bulletForce = 1000f;
    public float shotCooldown = 0.5f;
    public float currentCooldown = 0f;

    public int maxHealth = 100;
    [SyncVar(hook = nameof(OnHealthChanged))]
    public int curHealth = 100;

    [Command]
    public void CmdTakeDamage(int damage) {
        Debug.Log("Taking " + damage + " damage");
        curHealth -= damage;
    }
    
    void OnHealthChanged(int oldHealth, int newHealth) {
        Debug.Log("Health of a player changed from " + oldHealth + " to " + newHealth);
        if (isLocalPlayer) {
            Debug.Log("I took damage!");
        }
    }
    
    void Update() {
        if (!isLocalPlayer || !hasAuthority) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            CmdTakeDamage(10);
        }
        
        if (Input.GetMouseButton(0) && currentCooldown <= 0f) {
            CmdSpawnBullet();

            currentCooldown = shotCooldown;
        }

        if (currentCooldown > 0f) {
            currentCooldown -= Time.deltaTime;
        }
    }
    
    [Command]
    public void CmdSpawnBullet() {
        RpcShoot();
    }

    [ClientRpc]
    void RpcShoot() {
        GameObject bulletClone = Instantiate(bulletPrefab, bulletExit.transform.position, bulletExit.transform.rotation);
        bulletClone.GetComponent<Rigidbody>().velocity = bulletExit.transform.forward * bulletForce;
        
        Destroy(bulletClone, 7f);
    }
}