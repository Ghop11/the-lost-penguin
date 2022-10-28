using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{

    public GameObject pickupEffect;
    // Only using onTrigger then check if other.tag is Player

    private void OnTriggerEnter(Collider other)
    {

        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }
        
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            // would like gold coins worth more than silver coins
            PlayerStats.instance.AddExp(8); // need to figure out a good amount of exp
            
            // Need to add a coin counter and display it so the player
            //  can purchase memories or upgrade memories. 
            
            

        }
    }
}
