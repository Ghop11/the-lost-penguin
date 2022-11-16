using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : MonoBehaviour
{
    //UI canvas is getting convoluted, so I think it's better to have
    //skill UI be in its own script
    public Button button;
    public int cost;
    public int levelRequirement;
    public int skillChoiceToUpgrade;
    private int numberOfPurchases = 0;
    private const int PurchaseLimit = 3;
    private float currentStat;
    private float afterStat;
    public TMP_Text Description, LevelRequirementText, costText, beforeAndAfterText;

    enum Skill
    {
        JumpHeight,
        PebbleThrowSpeed,
        PebbleThrowRange,
        RunSpeed,
        ExpBoost
    }
    void Start()
    {
        DetermineSkill();
        UpdateSkillUINode();
    }

    public void PurchaseSkill()
    {
        //if(Input.GetButtonDown(string here))
        if(PlayerStats.instance.kentoLevel >= levelRequirement 
            && PlayerStats.instance.skillPoints - cost >= 0)
        {
            numberOfPurchases++;
            PlayerStats.instance.skillPoints -= cost;
            UIController.instance.UpdateSkillPoints();
            cost++; //increase cost for upgrades; 51 total skill points for all skills
            levelRequirement++;
            UpgradeSelectSkill();
            UpdateSkillUINode();
        }
    }

    private void SetUpgradeStats(float stat, float upgradeValue)
    {
        currentStat = stat;
        afterStat = stat + upgradeValue;
    }

    private void SetUpgradeStats(int stat, int upgradeValue)
    {
        currentStat = stat;
        afterStat = stat + upgradeValue;
    }

    private void UpgradeSkills(ref float stat, float upgradeValue)
    {
        stat += upgradeValue;
        SetUpgradeStats(stat, upgradeValue);
    }

    private void UpgradeSkills(ref int stat, int upgradeValue)
    {
        stat += upgradeValue;
        SetUpgradeStats(stat, upgradeValue);
    }
    private void DetermineSkill()
    {
        switch(skillChoiceToUpgrade)
        {
            case (int)Skill.JumpHeight:
            {
                SetUpgradeStats(PlayerController.instance.jumpForce, 2f);
                break;
            }
            case (int)Skill.PebbleThrowSpeed:
            {
                SetUpgradeStats(PlayerStats.instance.pebbleThrowSpeed, 2f);
                break;
            }
            case (int)Skill.PebbleThrowRange:
            {
                SetUpgradeStats(PlayerStats.instance.pebbleThrowDistance, 0.15f);
                break;
            }
            case(int)Skill.RunSpeed:
            {
                SetUpgradeStats(PlayerController.instance.moveSpeed, 1f);
                break;
            }
            case(int)Skill.ExpBoost:
            {
                SetUpgradeStats(PlayerStats.instance.expBoost, 2);
                break;
            }
        }
    }

    private void UpgradeSelectSkill()
    {
        switch(skillChoiceToUpgrade)
        {
            case (int)Skill.JumpHeight:
            {
                UpgradeSkills(ref PlayerController.instance.jumpForce, 2f);
                break;
            }
            case (int)Skill.PebbleThrowSpeed:
            {
                UpgradeSkills(ref PlayerStats.instance.pebbleThrowSpeed, 2f);         
                break;
            }
            case (int)Skill.PebbleThrowRange:
            {
                UpgradeSkills(ref PlayerStats.instance.pebbleThrowDistance, 0.15f);
                break;
            }
            case(int)Skill.RunSpeed:
            {
                UpgradeSkills(ref PlayerController.instance.moveSpeed, 1f);
                break;
            }
            case(int)Skill.ExpBoost:
            {
                UpgradeSkills(ref PlayerStats.instance.expBoost, 2);
                break;
            }
        }
    }

    private void UpdateSkillUINode()
    {
        if(numberOfPurchases >= PurchaseLimit)
        {
            beforeAndAfterText.text = "Max!";
            levelRequirement = 0;
            cost = 0;
        }           
        else
            beforeAndAfterText.text = $"{currentStat} --> {afterStat}";
        //description is done on Unity UI scene
        costText.text = $"Cost: {cost} point(s)";
        LevelRequirementText.text = $"Required Level: {levelRequirement}";
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            button.onClick.Invoke(); //setted Purchase skill to OnClick(), in inspector
        }
    }
}
