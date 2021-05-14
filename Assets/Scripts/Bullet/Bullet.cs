using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int damage = 5;

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<PlayerControl>().CmdTakeDamage(damage);
        }
    }
}