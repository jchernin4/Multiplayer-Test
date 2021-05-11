using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerNetworkController : NetworkBehaviour {
    public GameObject bulletPrefab;
    public GameObject bulletExit;
    public float bulletForce = 1000f;

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