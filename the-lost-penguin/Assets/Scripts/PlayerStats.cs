using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    //next task, show exp on the screen, near hp
    public int level = 1;
    public int currentExp = 0;
    public int[] expToNextLevel;
    public int maxLevel = 10; //default
    public int baseEXP = 5; //default
    public PlayerHealth playerHealth; //need to access max hp
    public int bellySlide;
    public int throwStrength;
    public int pebbleCount = 0;
    public int maxPeppleCount = 10;

    //anything else?


    
    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        //set exp level up requirements
        for(int i = 0; i < expToNextLevel.Length; i++)
        {
            //exp needed for next level increases at a multiple of 5
            //will change in the future once we know our enemy/leveling scale
            int exp = baseEXP * i;
            expToNextLevel[i] = exp;
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //when kento defeats an enemy, he should get exp
        //if he finds something, then he should get exp
        //both enemy and item scripts should have an exp field to be used
    }


    public void AddExp(int exp)
    {
        currentExp += exp;
        if(level < maxLevel)
        {
            if(currentExp >= expToNextLevel[level])
            {
                LevelUp();
                IncreaseStats();
            }
        }
        if(level <= maxLevel)
            currentExp = 0; 
    }

    private void LevelUp()
    {
        //put sound effect here that tells us we leveled up
        currentExp -= expToNextLevel[level];
        //update text on screen
        level++;
    }

    private void IncreaseStats()
    {
        bellySlide++; //distance or damage?
        throwStrength++; //distance and damage
        maxPeppleCount += 2;
        playerHealth.maxHealth += 10;
        //maybe set currentHealth = maxHealth for auto healing for leveling up
    }

    /*
    my role in the game
Items
Item system
Leveling up system
-Map Level Design
    */
}
