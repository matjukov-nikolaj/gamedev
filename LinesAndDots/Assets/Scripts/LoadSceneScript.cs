using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            /*CanvasGroup hints = GameObject.Find("ExitHints").GetComponent<CanvasGroup>();
            GameObject.Find("HintText").GetComponent<Text>().text = "You will lose one life";
            hints.alpha = 1f;
            hints.blocksRaycasts = true;*/
            int currentLoses = PlayerPrefs.GetInt("LOSES");
            currentLoses++;
            PlayerPrefs.SetInt("LOSES", currentLoses);
        }
        Application.LoadLevel("MainMenu");
    }
}
