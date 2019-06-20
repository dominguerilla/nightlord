using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles particle effects and mesh rendering when the servant enters/exits a level.
/// Should be attached to whatever has the PlayerController component.
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class ServantLoader : MonoBehaviour
{
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        player.onExit.AddListener(Disappear);
    }

    void Disappear(){
        gameObject.SetActive(false);
    }
}
