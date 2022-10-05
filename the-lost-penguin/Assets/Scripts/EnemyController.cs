using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {

    }


    // private void OnTriggerEnter(Collider other)
    // {
    //     // Write more code to NPC health, knocked-out, projectile, 
    //     if (other.tag == "Player")
    //     {
    //
    //         if (gameObject.tag == "EnemyHead")
    //         {
    //             Destroy(transform.parent.gameObject);
    //         }
    //
    //         if (gameObject.tag == "EnemyBody")
    //         {
    //             LevelManager.instance.Respawn();
    //         }
    //
    //
    //     
    //     }
    // }
    //
    // private void OnCollisionEnter(Collision collision)
    // {
    //     print("OnCollisionEnter");
    // }
    //
    //
    // private IEnumerator Stall()
    // {
    //     yield return new WaitForSeconds(5f);
    // }
}
