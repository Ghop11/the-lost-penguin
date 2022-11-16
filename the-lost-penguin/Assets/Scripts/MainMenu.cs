using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public string mapLevel;

    private void Awake()
    {
        AudioManager.instance.PlayOnlyWind();
    }

    public void StartGame()
    {
        
        SceneManager.LoadScene(mapLevel);
    }


}
