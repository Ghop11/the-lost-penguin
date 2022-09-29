using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    public Animator amin;
    public float waitBeforeRespawning;
    
    [HideInInspector]
    public bool respawning;

    public PlayerController player;

    public Vector3 respawnPoint;
    

    
    private void Awake()
    {
        instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        respawning = false;
        // player = FindObjectOfType<PlayerController>();
        respawnPoint = player.transform.position + Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Respawn()
    {
        if (!respawning)
        {
            respawning = true;
            player.updateKnockedOutStatus(true);
            amin.SetBool("isKnockedOut", true);
            UIController.instance.fadeToBlack();
            StartCoroutine(ResetKnockedOutStatus());
        }
    }

    private IEnumerator ResetKnockedOutStatus()
    {
        yield return new WaitForSeconds(waitBeforeRespawning);
        amin.SetBool("isKnockedOut", false);
        player.transform.position = respawnPoint;
        yield return new WaitForSeconds(0.2f); // need this so position doesn't update while moving Kento back to starting point
        UIController.instance.fadeFromBlack();
        player.updateKnockedOutStatus(false);
        respawning = false;
    }

}
