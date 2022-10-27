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
    private Transform pebbleHeight;


    private void Awake()
    {
        instance = this;

    }



    public void FirePebble()
    {
        pebbleHeight = PlayerController.instance.transform;
        var pebble = Instantiate(pebblePreFab, pebbleSpawnPoint.position, pebbleSpawnPoint.rotation);
        // var pebble = Instantiate(pebblePreFab, pebbleHeight.position, pebbleHeight.rotation);
        pebble.GetComponent<Rigidbody>().velocity = pebbleSpawnPoint.forward * pebbleSpeed;
        print("Throwing the pebble");
    }



}
