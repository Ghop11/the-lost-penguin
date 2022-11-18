using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector]
    public static PlayerStats instance;
    public int kentoLevel = 1;
    public int maxLevel = 10; //default max level, could change in the future
    public int currentExp = 0;
    //the exp value that is needed to be reached so that kento levels up
    public int expForNextLevel {get; private set;}
    //range of level values for reaching next sequential levels
    public int[] expToNextLevelList { get; private set; }
    public float pebbleThrowDistance;
    public float pebbleThrowSpeed;
    public int pebbleCount;
    public int maxPeppleCount = 10;
    public int expBoost;
    public int skillPoints = 10; //can be gained on level up, or completing objectives in the story
    
    // Start is called before the first frame update
    void Start()
    {
        int baseEXP = 5; //default
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
        DebugExp();
    }

    private void DebugExp()
    {
        if(Input.GetKey(KeyCode.J))
            AddExp(1);
        print("Exp Debugging");
    }
    public void Awake()
    {
        instance = this;
    }

    //from either goons or collectables
    public void AddExp(int exp)
    {
        currentExp += exp + expBoost;
        print("gaining exp");
        UIController.instance.UpdateExpDisplay();
        if(kentoLevel < maxLevel)
        {
            if(currentExp >= expToNextLevelList[kentoLevel - 1])
            {
                AudioManager.instance.PlaySFX(15, 0.1f);
                LevelUp();
                IncreaseStats();       
            }
        }
        //if we're at max level, don't gain anymore exp
        if(kentoLevel >= maxLevel)
        {
            //setting this to 0 is needed, otherwise the exp number 
            //will keep rising despite being at max
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
        if(kentoLevel % 2 == 0)
        {
            if(kentoLevel == maxLevel)
                skillPoints++; //would reach number of skill points needed to get everything
            else
                skillPoints += 10; //gain points every 2 levels
        }
                 
        expForNextLevel = expToNextLevelList[kentoLevel - 1];
        UIController.instance.UpdateExpLevel();
    }

    private void IncreaseStats()
    {
        maxPeppleCount += 2;

        //update display, as well as some healing for leveling up
        PlayerHealth.instance.maxHealth += 10;
        PlayerHealth.instance.FillHealth(); // when player levels up they will get full health
        UIController.instance.UpdateHealthDisplay(PlayerHealth.instance.currentHealth);
        UIController.instance.UpdateExpDisplay();
        UIController.instance.UpdatePebbleCount();
    }

    public void PebbleThrown()
    {
        if (pebbleCount - 1 >= 0)
        {
            pebbleCount -= 1;
            UIController.instance.UpdatePebbleCount();
        }

    }

    public void PebblePickedUp(int amount)
    {
        if (pebbleCount + amount > maxPeppleCount)
        {
            pebbleCount = maxPeppleCount;
        }
        else
        {
            pebbleCount += amount;
        }
        UIController.instance.UpdatePebbleCount();
    }


}
