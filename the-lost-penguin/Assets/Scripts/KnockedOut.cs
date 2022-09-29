using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedOut : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Write more code to NPC health, knocked-out, projectile, 
        if (other.tag == "Player")
        {
            
            // other.gameObject.GetComponent<CharacterController>().Move(Vector3.up - other.transform.position);
            LevelManager.instance.Respawn();
        }
    }

}
