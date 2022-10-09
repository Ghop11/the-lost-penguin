using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats instance;
    //next task, show exp on the screen, near hp
    public int kentoLevel = 1;
    public int currentExp = 0;
    //the exp value that is needed to be reached so that kento levels up
    public int expForNextLevel;
    //range of levels that contains data to reach sequential levels
    public int[] expToNextLevelList;
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
        expToNextLevelList = new int[maxLevel];
        //set exp level up requirements
        for(int i = 0; i < expToNextLevelList.Length; i++)
        {
            //exp needed for next level increases at a multiple of 5
            //will change in the future once we know our enemy/leveling scale
            int exp = baseEXP * i;
            expToNextLevelList[i] = exp;
            
        }
        expForNextLevel = expToNextLevelList[kentoLevel];

        //update UI, start at 0 experience
        UIController.instance.UpdateExpDisplay(0);
    }

    // Update is called once per frame
    void Update()
    {
        //when kento defeats an enemy, he should get exp
        //if he finds something, then he should get exp
        //both enemy and item scripts should have an exp field to be used
    }

    public void Awake()
    {
        instance = this;
    }

    //from either goons or collectables
    public void AddExp(int exp)
    {
        currentExp += exp;
        if(kentoLevel < maxLevel)
        {
            if(currentExp >= expToNextLevelList[kentoLevel])
            {
                LevelUp();
                IncreaseStats();
                UIController.instance.UpdateExpDisplay(0);
            }
        }
        if(kentoLevel <= maxLevel)
            currentExp = 0; 
    }

    private void LevelUp()
    {
        //put sound effect here that tells us we leveled up
        currentExp -= expToNextLevelList[kentoLevel];
        //update text on screen
        kentoLevel++;
        expForNextLevel = expToNextLevelList[kentoLevel];
    }

    private void IncreaseStats()
    {
        bellySlide++; //distance or damage?
        throwStrength++; //distance and damage
        maxPeppleCount += 2;
        playerHealth.maxHealth += 10;
        //maybe set currentHealth = maxHealth for auto healing for leveling up
    }
}
