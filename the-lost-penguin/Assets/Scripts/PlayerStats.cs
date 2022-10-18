using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //[HideInInspector]
    public static PlayerStats instance;
    public int kentoLevel = 1;
    public int currentExp = 0;
    //the exp value that is needed to be reached so that kento levels up
    public int expForNextLevel;
    //range of level values for reaching next sequential levels
    public int[] expToNextLevelList;
    public int maxLevel = 10; //default max level, could change in the future
    [HideInInspector]
    private int baseEXP = 5; //default
    //public PlayerHealth playerHealth; //need to access max hp
    public int throwStrength;
    public int pebbleCount = 0;
    public int maxPeppleCount = 10;

    //anything else we could add?


    
    // Start is called before the first frame update
    void Start()
    {
        expToNextLevelList = new int[maxLevel];
        //set exp level up requirements
        expToNextLevelList[0] = baseEXP; //first level set to 5 exp
        for(int i = 1; i < expToNextLevelList.Length - 1; i++)
        {
            //next levels will
            //exp needed for next level increases at a multiple of 5
            //will change in the future once we know our enemy/leveling scale
            int exp = baseEXP * i * 2;
            expToNextLevelList[i] = exp;
            
        }
        //at max level, the "next" level requirement is 0 exp,
        expToNextLevelList[expToNextLevelList.Length - 1] = 0;

        expForNextLevel = expToNextLevelList[kentoLevel - 1];
        //update UI, start at 0 experience
        UIController.instance.UpdateExpDisplay();
        UIController.instance.UpdateExpLevel();
        UIController.instance.UpdatePebbleCount();
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
            }
        }
        //if we're at max level, don't gain anymore exp
        if(kentoLevel >= maxLevel)
        {
            currentExp = 0;
            UIController.instance.UpdateExpDisplay();
        }         
    }

    private void LevelUp()
    {
        //put sound effect here that tells us we leveled up
        currentExp -= expToNextLevelList[kentoLevel - 1];
        //update text on screen
        kentoLevel++;
        expForNextLevel = expToNextLevelList[kentoLevel - 1];
        UIController.instance.UpdateExpLevel();
    }

    private void IncreaseStats()
    {
        throwStrength++; //distance and damage
        maxPeppleCount += 2;

        //update display, as well as some healing for leveling up
        PlayerHealth.instance.maxHealth += 10;
        PlayerHealth.instance.AddHealth(5);
        UIController.instance.UpdateExpDisplay();
        UIController.instance.UpdatePebbleCount();
    }
}
