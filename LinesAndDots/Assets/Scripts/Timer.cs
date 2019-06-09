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
    public float time;

    public float timeLeft;

    public bool isGameOver = false;
    
    public bool win = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SampleScene2")
        {
            Cursor.lockState = CursorLockMode.None;
        }
        isGameOver = false;
        int resultWins = PlayerPrefs.GetInt("WINS");
        int resultLoses = PlayerPrefs.GetInt("LOSES");
        int currentLevel = resultWins + resultLoses;
        float diff = 1.0f;
        if (resultLoses > 0)
        {
            diff = resultWins / resultLoses;
        }

        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
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
                time = timeLeft;
            }
            else if (currentLevel > 0 && currentLevel <= 150)
            {
                timeLeft = 10;
                slider.maxValue = 10;
                time = timeLeft;
            }
            else
            {
                timeLeft = 15;
                slider.maxValue = 15;
                time = timeLeft;
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

        if (timeLeft > 0 &&!isGameOver)
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
            int resultWins = PlayerPrefs.GetInt("WINS");
            int resultLoses = PlayerPrefs.GetInt("LOSES");
            if (win)
            {
                ++resultWins;
                PlayerPrefs.SetInt("WINS", resultWins);
            }
            else
            {
                ++resultLoses;
                PlayerPrefs.SetInt("LOSES", resultLoses);
            }

            int currentLevel = resultWins + resultLoses;
            if (currentLevel == Int32.MaxValue)
            {
                PlayerPrefs.SetInt("WINS", 0);
                PlayerPrefs.SetInt("LOSES", 0);
            }

            CanvasGroup resultScreen = GameObject.Find("CanvasGroup").GetComponent<CanvasGroup>();
            SpriteRenderer blur = GameObject.Find("Blur").GetComponent<SpriteRenderer>();
            Text resultText = GameObject.Find("ResultText").GetComponent<Text>();
            resultScreen.alpha = 1f;
            resultScreen.blocksRaycasts = true;
            if (win)
            {
                blur.color = new Color(0.0f, 255.0f, 0.0f, 0.2f);
                if (timeLeft > (time / 3)) 
                {
                    resultText.text = "You are so fast! Win +3";
                    resultWins += 2;
                    PlayerPrefs.SetInt("WINS", resultWins);
                    Image screenshot = GameObject.Find("ScreenShot").GetComponent<Image>();
                    screenshot.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                    AudioSource audioSource1 = GameObject.Find("WinAudio").GetComponent<AudioSource>();
                    audioSource1.Play(0);
                }
                else
                {
                    resultText.text = "Win!";
                    Image screenshot = GameObject.Find("ScreenShot").GetComponent<Image>();
                    screenshot.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                    AudioSource audioSource = GameObject.Find("WinAudio").GetComponent<AudioSource>();
                    audioSource.Play(0);
                }
            }
            else
            {
                Image screenshot = GameObject.Find("ScreenShot").GetComponent<Image>();
                byte[] bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + "ScreenshotManager.png");
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(bytes);
                Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f) );
                screenshot.sprite = sp;
                screenshot.color = new Color(255.0f, 255.0f, 255.0f, 0.2f);
                RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
                screenshot.GetComponent<RectTransform>().rect.Set(424, 436, canvasRect.rect.width, canvasRect.rect.height);
                blur.color = new Color(255.0f, 0.0f, 0.0f, 0.2f);
                resultText.text = "Lose!";
                AudioSource audioSource = GameObject.Find("LoseAudio").GetComponent<AudioSource>();
                audioSource.Play(0);
            }

            isGameOver = true;
            return;
        }
        var sm = GameObject.Find("ScreenshotManager").GetComponent<SaveScreen>();
        sm.SaveScreenTexture();
        Application.LoadLevel("SampleScene2");
    }
    private float GetScale(int width, int height, Vector2 scalerReferenceResolution, float scalerMatchWidthOrHeight)
    {
        return Mathf.Pow(width/scalerReferenceResolution.x, 1f - scalerMatchWidthOrHeight)*
               Mathf.Pow(height/scalerReferenceResolution.y, scalerMatchWidthOrHeight);
    }
    public bool GetGameResult()
    {
        GameObject emptyObject = GameObject.Find("EmptyObject");
        List<String> visitedDots = emptyObject.GetComponent<EmptyObject>().GetVisitedDots();
        List<String> requiredDots = LevelGenerator.GetGeneratedListStr();
        List<bool> isContains = new List<bool>(requiredDots.Count);
        if (requiredDots.Count == visitedDots.Count)
        {
            for (int i = 0; i < requiredDots.Count; i++)
            {
                isContains.Add(requiredDots[i] == visitedDots[i] ? true : false);
            }
        }
        
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