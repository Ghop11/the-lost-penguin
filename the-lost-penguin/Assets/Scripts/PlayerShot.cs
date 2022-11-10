using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public static PlayerShot instance;
    
    public Transform pebbleSpawnPoint;
    public GameObject pebblePreFab;
    public float pebbleSpeed;
    public Rigidbody theRB;
    
    private void Awake()
    {
        instance = this;
    }
    

    public void FirePebble()
    {

        var pebble = Instantiate(pebblePreFab, pebbleSpawnPoint.position, pebbleSpawnPoint.rotation);
        pebble.GetComponent<Rigidbody>().velocity = pebbleSpawnPoint.forward * PlayerStats.instance.pebbleThrowSpeed;
        print("Throwing the pebble");
    }
}