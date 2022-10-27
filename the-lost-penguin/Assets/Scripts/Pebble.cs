using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : MonoBehaviour
{
    public float life;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.tag != "Level")
        // {
        //     Destroy(collision.gameObject);
        //     Destroy(gameObject);
        // }
    }
}
