using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PickUpThrowables : MonoBehaviour
{
    public GameObject throwablePreFab;
    public float defaultTimer;
    
    public Transform spawnThrowablePoint;
    
    
    void Start()
    {
        
        spawnThrowablePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats.instance.PebblePickedUp(5);
            // startTimer = true;
            // respawnTimer = defaultTimer;
            // StartCoroutine(WaitToRespawn());
            // gameObject.SetActive(false);
            TimerCountDown.instance.RespawnMoreThrowable(spawnThrowablePoint, throwablePreFab);
            Destroy(gameObject);
        }
    }
}
