using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedOut : MonoBehaviour
{
    public Animator amin;
    public float waitBeforeRespawning;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // pc.updateKnockedOutStatus(true);
            PlayerController.instance.updateKnockedOutStatus(true);
            amin.SetBool("isKnockedOut", true);
            StartCoroutine(ResetKnockedOutStatus());
        }
    }


    private IEnumerator ResetKnockedOutStatus()
    {
        yield return new WaitForSeconds(waitBeforeRespawning);
        amin.SetBool("isKnockedOut", false);
        PlayerController.instance.updateKnockedOutStatus(false);
    }

}
