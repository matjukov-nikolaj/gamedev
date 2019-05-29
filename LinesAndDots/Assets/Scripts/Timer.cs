using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private Dictionary<string, string> results = GameResult.GetParameters();

    public float time;

    public float timeLeft;

    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        Scene scene = SceneManager.GetActiveScene();
        int resultWins = Int32.Parse(results["WINS"]);
        int resultLoses = Int32.Parse(results["LOSES"]);
        int currentLevel = resultWins + resultLoses;
        float diff = 1.0f;
        if (resultLoses > 0)
        {
            diff = resultWins / resultLoses;
        }

        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
        time = timeLeft;
        if (currentLevel <= 3)
        {
            timeLeft += 2;
        }

        if (scene.name == "SampleScene")
        {
            if (currentLevel > 500 && diff > 0.5)
            {
                timeLeft = 20;
                slider.maxValue = 20;
                return;
            }

            if (currentLevel > 0 && currentLevel <= 150)
            {
                timeLeft = 5;
                slider.maxValue = 5;
            }
            else if (currentLevel > 150 && currentLevel <= 500)
            {
                timeLeft = 8;
                slider.maxValue = 8;
            }
            else
            {
                timeLeft = 7;
                slider.maxValue = 7;
            }
        }

        if (scene.name == "SampleScene2")
        {
            if (currentLevel > 500 && diff > 0.5)
            {
                timeLeft = 30;
                slider.maxValue = 30;
            }
            else if (currentLevel > 0 && currentLevel <= 150)
            {
                timeLeft = 10;
                slider.maxValue = 10;
            }
            else
            {
                timeLeft = 15;
                slider.maxValue = 15;
            }

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
        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();

        slider.value = timeLeft;

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }

        else if (timeLeft > -100 && timeLeft < 0)
        {
            timeLeft = 0;
        }

        if (Math.Abs(timeLeft) == 0.0f)
        {
            GameOver();
            timeLeft -= 200;
        }
    }

    public void GameOver()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SampleScene2")
        {
            Console.WriteLine("GameOver");
            bool win = GetGameResult();
            int resultWins = 0;
            int resultLoses = 0;
            if (win)
            {
                resultWins = Int32.Parse(results["WINS"]);
                ++resultWins;
                results["WINS"] = resultWins.ToString();
            }
            else
            {
                resultLoses = Int32.Parse(results["LOSES"]);
                ++resultLoses;
                results["LOSES"] = resultLoses.ToString();
            }

            int currentLevel = resultWins + resultLoses;
            if (currentLevel == Int32.MaxValue)
            {
                results["LOSES"] = "0";
                results["WINS"] = "0";
                File.WriteAllLines("Assets/game.properties",
                    results.Select(element => element.Key + "=" + element.Value).ToArray());
            }

            File.WriteAllLines("Assets/game.properties",
                results.Select(element => element.Key + "=" + element.Value).ToArray());
            CanvasGroup resultScreen = GameObject.Find("CanvasGroup").GetComponent<CanvasGroup>();
            SpriteRenderer blur = GameObject.Find("Blur").GetComponent<SpriteRenderer>();
            Text resultText = GameObject.Find("Text").GetComponent<Text>();
            resultScreen.alpha = 1f;
            resultScreen.blocksRaycasts = true;
            if (win)
            {
                blur.color = new Color(0.0f, 255.0f, 0.0f, 0.2f);
                if (timeLeft > (time / 3))
                {
                    resultText.text = "You are so fast! Win +3";
                    AudioSource audioSource1 = GameObject.Find("WinAudio").GetComponent<AudioSource>();
                    audioSource1.Play(0);
                }
                else
                {
                    resultText.text = "Win!";
                    AudioSource audioSource = GameObject.Find("WinAudio").GetComponent<AudioSource>();
                    audioSource.Play(0);
                }
            }
            else
            {
                blur.color = new Color(255.0f, 0.0f, 0.0f, 0.2f);
                resultText.text = "Lose!";
                AudioSource audioSource = GameObject.Find("LoseAudio").GetComponent<AudioSource>();
                audioSource.Play(0);
            }

            isGameOver = true;
            return;
        }

        Application.LoadLevel("SampleScene2");
    }

    public bool GetGameResult()
    {
        GameObject emptyObject = GameObject.Find("EmptyObject");
        List<String> visitedDots = emptyObject.GetComponent<EmptyObject>().GetVisitedDots();
        List<String> requiredDots = LevelGenerator.GetGeneratedListStr();
        List<bool> isContains = new List<bool>(requiredDots.Count);
        foreach (var requiredDot in requiredDots)
        {
            isContains.Add(visitedDots.Contains(requiredDot) ? true : false);
        }

        bool win = false;
        if (requiredDots.Count == visitedDots.Count)
        {
            foreach (var isContain in isContains)
            {
                if (!isContain)
                {
                    win = false;
                    break;
                }

                win = true;
            }
        }

        return win;
    }
}