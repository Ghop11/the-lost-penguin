using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewLevel : MonoBehaviour
{

    public static int LevelNeededToAdvance = 7;
    public GameObject expectedText;
    private String needToLevelUp = "You must be at level" + LevelNeededToAdvance + " to escape";
    private String nextMap = "You have reached the end of the beta";
    public TMP_Text message;
    
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            // check players level to escape this map: 
            if (PlayerStats.instance.kentoLevel >= LevelNeededToAdvance)
            {
                message.text = nextMap;
            }
            else
            {
                message.text = needToLevelUp;
            }
            // show text
            expectedText.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // hide text
            expectedText.SetActive(false);
        }
    }
    
}
