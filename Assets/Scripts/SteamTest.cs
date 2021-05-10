using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class SteamTest : MonoBehaviour {
    void Start() {
        if (!SteamManager.Initialized) {
            return;
        }

        string personaName = SteamFriends.GetPersonaName();
        
        Debug.Log("Name: " + personaName);
    }
}