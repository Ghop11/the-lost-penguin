using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedOut : MonoBehaviour
{
    public Animator amin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            amin.SetBool("isKnockedOut", true);
        }
    }
}
