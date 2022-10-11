using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int kentoLevel = 1;
    public int currentExp = 0;
    //the exp value that is needed to be reached so that kento levels up
    public int expForNextLevel;
    //range of level values for reaching next sequential levels
    public int[] expToNextLevelList;
    public int maxLevel = 10; //default max level, could change in the future
    private int baseEXP = 5; //default
    //public PlayerHealth playerHealth; //need to access max hp
    public int bellySlide;
    public int throwStrength;
    public int pebbleCount = 0;
    public int maxPeppleCount = 10;

    //anything else we could add?


    
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
        UIController.instance.UpdateExpDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        //when kento defeats an enemy, he should get exp
        //if he finds something, then he should get exp
        //both enemy and item scripts should have an exp field to be used

        //testing, remove when done
        if(Input.GetKey(KeyCode.J))
        {
            AddExp(1);
        }
    }

    public void Awake()
    {
        instance = this;
    }

    //from either goons or collectables
    public void AddExp(int exp)
    {
        currentExp += exp;
        UIController.instance.UpdateExpDisplay();
        if(kentoLevel < maxLevel)
        {
            if(currentExp >= expToNextLevelList[kentoLevel - 1])
            {
                LevelUp();
                IncreaseStats();
                UIController.instance.UpdateExpDisplay();
            }
        }
        //if we're at max level, don't gain anymore exp
        if(kentoLevel >= maxLevel)
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
        bellySlide++; //slide distance or damage?
        throwStrength++; //distance and damage
        maxPeppleCount += 2;
        PlayerHealth.instance.maxHealth += 10;
        //update max health display, as well as some healing for leveling up
        UIController.instance.UpdateHealthDisplay(PlayerHealth.instance.currentHealth + 5);
        
    }
}
