using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class UIController : MonoBehaviour
{
    [HideInInspector]
    public static UIController instance;

    public Image fadeScreen;
    private bool isFadingToBlack;
    private bool isFadingFromBlack;
    public float fadeSpeed = 2f;
    
    // for health bar
    public Slider healthBar;
    public TMP_Text healthText;
    public TMP_Text lives;

    // exp bar
    public Slider expBar;
    public TMP_Text expText;
    public TMP_Text expLevelText;

    //pebbles
    public TMP_Text pebblesText;

    //stats menu data
    public GameObject statsMenu;
    //skill tree menu data
    public GameObject skillTreeMenu;
    public TMP_Text[] statsData;

    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //start with stats and skill tree menu closed
        statsMenu.SetActive(false);
        skillTreeMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
        
        if (isFadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        OpenOrCloseStats();

        OpenOrCloseSkillTree();
    }


    public void fadeToBlack()
    {
        isFadingToBlack = true;
        isFadingFromBlack = false;
    }

    public void fadeFromBlack()
    {
        isFadingToBlack = false;
        isFadingFromBlack = true;
    }
    
    
    // update Health UI
    public void UpdateHealthDisplay(int currentHealth)
    {
        healthBar.maxValue = PlayerHealth.instance.maxHealth;
        healthBar.value = currentHealth;

        healthText.text = "Health: " + currentHealth + "/" + PlayerHealth.instance.maxHealth;      
    }


    // update lives 
    public void UpdatePlayersLives(int value)
    {
        // negative value when a player gets knocked out, a positive value when a player gains a life
        lives.text = "Lives: " + (PlayerHealth.instance.lives + value);
    }

    //update experience points
    public void UpdateExpDisplay(/*int exp*/)
    {
        //int addedExp = PlayerStats.instance.currentExp + exp;
        expBar.maxValue = PlayerStats.instance.expForNextLevel;
        expBar.value = PlayerStats.instance.currentExp;
        expText.text = $"Exp: {PlayerStats.instance.currentExp} / {PlayerStats.instance.expForNextLevel}";   
    }

    public void UpdateExpLevel()
    {
        expLevelText.text = $"Lvl: {PlayerStats.instance.kentoLevel}";
    }

    public void UpdatePebbleCount()
    {
        pebblesText.text = $"Pebbles: {PlayerStats.instance.pebbleCount}/{PlayerStats.instance.maxPeppleCount}";
    }

    private void UpdateStatsOnMenu()
    {
        statsData[0].text = $"{PlayerHealth.instance.currentHealth}";
        statsData[1].text = PlayerHealth.instance.maxHealth.ToString();
        statsData[2].text = $"{PlayerStats.instance.pebbleCount}";
        statsData[3].text = PlayerStats.instance.maxPeppleCount.ToString();
        statsData[4].text = $"{PlayerStats.instance.pebbleThrowDistance}";
        statsData[5].text = $"{PlayerStats.instance.pebbleThrowSpeed}"; //there's probably a variable like this in a script somewhere...
        statsData[6].text = $"{PlayerController.instance.moveSpeed}";
        statsData[7].text = $"{PlayerController.instance.jumpForce}";
        statsData[8].text = $"{PlayerStats.instance.expBoost}";
        statsData[9].text = $"{PlayerStats.instance.kentoLevel}";
        statsData[10].text = $"Current Exp: {PlayerStats.instance.currentExp}";
        statsData[11].text = $"Exp for Level up: {PlayerStats.instance.expForNextLevel - PlayerStats.instance.currentExp}";
    }

    //TODO: Pause game when menu is open
    private void OpenOrCloseStats()
    {
        //stats menu opening and closing
        if(Input.GetKeyDown(KeyCode.M))
        {
            //don't display the skill tree with the stats open
            if(!skillTreeMenu.activeInHierarchy)
            {
                if(statsMenu.activeInHierarchy)
                    statsMenu.SetActive(false);
                else
                {
                    UpdateStatsOnMenu();
                    statsMenu.SetActive(true);
                }
            }    
        }
    }

    private void OpenOrCloseSkillTree()
    {
        //skill tree menu opening and closing
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(!statsMenu.activeInHierarchy)
            {
                if(skillTreeMenu.activeInHierarchy)
                {
                    skillTreeMenu.SetActive(false);
                }   
                else
                {
                    SkillUI.instance.UpdateSkillTreeUI();
                    skillTreeMenu.SetActive(true);
                }
            }        
        }
    }
}
