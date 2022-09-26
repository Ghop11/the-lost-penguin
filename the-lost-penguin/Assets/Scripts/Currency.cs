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
        }
    }
}
