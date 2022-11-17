using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MemoryOwl : MonoBehaviour
{
    public static int getMoreAdvice = 5;
    public GameObject expectedText;
    // private String needToLevelUp = "You must be at level" + LevelNeededToAdvance + " to escape";
    // private String nextMap = "You have reached the end of the beta";
    private string messageText;
    public TMP_Text message;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            // check players level to escape this map: 
            if (PlayerStats.instance.kentoLevel < getMoreAdvice)
            {
                FirstMessage();
            }
            else
            {
                SecondMessage();
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


    public void FirstMessage()
    {
        messageText = @"""Hi again Kento, it’s me! Were you able to stop Tux?""" + System.Environment.NewLine + System.Environment.NewLine
            + @"""...you don't know me?""" + System.Environment.NewLine + System.Environment.NewLine
            + @"""Let me introduce myself. I am the Oracle. I give advice to travelers who fight for good. Kento, you came to me 2 days ago to ask for my advice on how to stop Tux from taking the community food supply. But my advice would be useless now as you are nowhere near as powerful as you once were""" +
            System.Environment.NewLine + System.Environment.NewLine
            + @"""Get stronger by leveling up by collecting coins and knocking out those who try to stop you. Return to me at level 5""";

        message.text = messageText;
    }
    
    public void SecondMessage()
    {
        messageText = @"""Hi again Kento, you have gotten stronger""" + System.Environment.NewLine + System.Environment.NewLine 
            + @"""You should press 'N' the on your keyboard to increase your Skills. """ + System.Environment.NewLine + System.Environment.NewLine
            + @"""Next, find the location by ice pond where there are other owls that look like me. There you should be able to escape this dangerous area.""";

        message.text = messageText;
    }
    
    
}
