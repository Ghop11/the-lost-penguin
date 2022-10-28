using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class UIController : MonoBehaviour
{

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
    public TMP_Text[] healthData, expData, pebblesData, miscData;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //start with stats menu closed
        statsMenu.SetActive(false);
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

        //stats menu opening and closing
        if(Input.GetKeyDown(KeyCode.M))
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
        healthData[0].text = $"Current HP: {PlayerHealth.instance.currentHealth}";
        healthData[1].text = $"Max HP: {PlayerHealth.instance.maxHealth}";

        expData[0].text = $"Level: {PlayerStats.instance.kentoLevel}";
        expData[1].text = $"Current Exp: {PlayerStats.instance.currentExp}";
        expData[2].text = $"Exp Needed for Level Up: {PlayerStats.instance.expForNextLevel}";

        pebblesData[0].text = $"Current Amount: {PlayerStats.instance.pebbleCount}";
        pebblesData[1].text = $"Max Amount: {PlayerStats.instance.maxPeppleCount}";
        pebblesData[2].text = $"Throw Range: {PlayerStats.instance.pebbleThrowDistance}";
        pebblesData[3].text = $"Throw Speed: {PlayerShot.instance.pebbleSpeed}";

        miscData[0].text = $"Run Speed: {PlayerController.instance.moveSpeed}";
        miscData[1].text = $"Jump Height: {PlayerController.instance.jumpForce}";
        miscData[2].text = $"Exp Boost: {PlayerStats.instance.expBoost}";
    }
}
