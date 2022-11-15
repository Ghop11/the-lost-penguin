using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        //we'll put in a different sfx for eating a fish at some point
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }

        //TODO: if kento is at full hp, don't heal and destroy object
        //instead, lower it's opacity until kento leaves the sphere collider's radius
        
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.AddHealth(10);
            AudioManager.instance.PlaySFX(4, 0.1f);
            //other.gameObject.SetActive(false);            
            Destroy(gameObject);
            
            // give exp points 
            PlayerStats.instance.AddExp(2); // need to figure out a good amount of exp

            
        }
    }
}
