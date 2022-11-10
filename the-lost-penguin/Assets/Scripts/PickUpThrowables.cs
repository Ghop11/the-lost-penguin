using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrowables : MonoBehaviour
{
    public float respawnThrowables;
    public Transform throwablesSpawnPoint;
    public GameObject throwablePreFab;
    private float respawnTimer;
    public float defaultTimer;

    private Boolean startTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        throwablesSpawnPoint = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            if (respawnTimer >= 0)
            {
                print("respawn timer going");
                respawnTimer -= Time.deltaTime;
            }
            else
            {
                startTimer = false;
                Respawn();
            }
        }
        
    }

    private void Respawn()
    {
        var pickUps = Instantiate(throwablePreFab, throwablesSpawnPoint.position, throwablesSpawnPoint.rotation);
    }
    
   
        

    
    // need this or frame rate will automatically remove 10HP from kento before respawning
    private IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(5);
        Respawn();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats.instance.PebblePickedUp(5);
            startTimer = true;
            respawnTimer = defaultTimer;
            Destroy(gameObject);
        }
    }
}
