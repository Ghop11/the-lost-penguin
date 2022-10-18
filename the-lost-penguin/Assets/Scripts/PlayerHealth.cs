using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public static PlayerHealth instance;
    private int currentHealth;
    public int maxHealth;

    private float noDamageCounter;
    public float noDamageLength = 1f;

    public int lives;

    public void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth); // updating UI to have default values
        UIController.instance.UpdatePlayersLives(0); //  showing how many lives a player has
    }

    // Update is called once per frame
    void Update()
    {
        if (noDamageCounter > 0)
        {
            noDamageCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int value = 10)
    {
        if (noDamageCounter <= 0)
        {
            noDamageCounter = noDamageLength;
            
            // Need to add some kind of display that shows Kento took damage and additional damage does not take place
        
            if (currentHealth - value <= 0)
            {   
                // Kento is knocked out
                KnockedOut();
            }
            
            currentHealth -= value;
            //found a display issue where Kento's Health goes into the negatives
            //if his health isn't a multiple of 10, 
            //setting his health to be strictly 0 will fix this
            if(currentHealth < 0)
                currentHealth = 0;
            UIController.instance.UpdateHealthDisplay(currentHealth);
        }
    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
    }

    // a function to increase player health when health is low. 
    public void AddHealth(int value)
    {
        if (currentHealth + value < maxHealth)
        {
            currentHealth += value;
        }
        else
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay(currentHealth);
    }
    
    // Need a function to add a players life
    
    
    // Need a function to remove players life
    
    
    // kento is knocked out
    public void KnockedOut()
    {
        // Needs to respawn
        LevelManager.instance.Respawn();
        
        // needs to get full health
        StartCoroutine(SetFullHealthAfterKnockedOut());
        
        // needs to lose a life
        // write this code later. 

    }
    
    // need this or frame rate will automatically remove 10HP from kento before respawning
    private IEnumerator SetFullHealthAfterKnockedOut()
    {
        yield return new WaitForSeconds(1);
        FillHealth();
        UIController.instance.UpdateHealthDisplay(currentHealth);
        UIController.instance.UpdatePlayersLives(-1);
    }

}
