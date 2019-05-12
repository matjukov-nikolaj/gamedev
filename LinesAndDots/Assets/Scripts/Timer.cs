using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float timeLeft = 5.0f;
    public Slider timer;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SampleScene2")
        {
            CanvasGroup resultScreen = GameObject.Find("CanvasGroup").GetComponent<CanvasGroup>();
            resultScreen.alpha = 0f;
            resultScreen.blocksRaycasts = false;
            SpriteRenderer blur = GameObject.Find("Blur").GetComponent<SpriteRenderer>();
            blur.color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Slider").GetComponent<Slider>().value = timeLeft;
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SampleScene2")
        {
            Console.WriteLine("GameOver");
            GameObject emptyObject = GameObject.Find("EmptyObject");
            List<String> visitedDots = emptyObject.GetComponent<EmptyObject>().GetVisitedDots();
            
            List<String> requiredDots = LevelGenerator.GetGeneratedListStr();

            bool win = false;
            foreach (var requiredDot in requiredDots)
            {
                win = visitedDots.Contains(requiredDot) ? true : false;
            }
            CanvasGroup resultScreen = GameObject.Find("CanvasGroup").GetComponent<CanvasGroup>();
            resultScreen.alpha = 1f;
            resultScreen.blocksRaycasts = true;
            if (win)
            {
                SpriteRenderer blur = GameObject.Find("Blur").GetComponent<SpriteRenderer>();
                blur.color = Color.green;
//                blur.color = new Color(0.0, 255.0, 0.0, 0.2);
            }
            
            return;
        }
        Application.LoadLevel("SampleScene2");

    }
}
