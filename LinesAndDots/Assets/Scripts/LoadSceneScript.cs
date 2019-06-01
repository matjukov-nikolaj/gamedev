using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneScript : MonoBehaviour
{
    public string sceneName;
    
    public void GameStart()
    {
        Application.LoadLevel(sceneName);
    }
    
    public void MainMenu()
    {
        if (!GameObject.Find("Slider").GetComponent<Timer>().isGameOver)
        {
            
            int currentLoses = PlayerPrefs.GetInt("LOSES");
            currentLoses++;
            PlayerPrefs.SetInt("LOSES", currentLoses);
        }
        Application.LoadLevel("MainMenu");
    }
}
