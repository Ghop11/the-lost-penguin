using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;


    public Image fadeScreen;
    private bool isFadingToBlack;
    private bool isFadingFromBlack;
    public float fadeSpeed = 2f;
    
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
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
    
    
    
    
    
    
    
    
}