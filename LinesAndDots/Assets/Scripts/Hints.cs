using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Hints : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(BetweenFunc());
        }

        IEnumerator<WaitForSeconds> BetweenFunc()
        {
            StartCoroutine(ShowHelpBox());
            yield return new WaitForSeconds(2.0f);
        }

        void Update()
        {

        }

        IEnumerator<WaitForSeconds> ShowHelpBox()
        {
            int resultWins = PlayerPrefs.GetInt("WINS");
            int resultLoses = PlayerPrefs.GetInt("LOSES");
            int currentLevel = resultWins + resultLoses;
            CanvasGroup resultScreen = GameObject.Find("Hints").GetComponent<CanvasGroup>();
            if (currentLevel <= 3)
            {
                resultScreen.alpha = 1f;
                resultScreen.blocksRaycasts = true;
                yield return new WaitForSeconds(2);
                resultScreen.alpha = 0f;
                resultScreen.blocksRaycasts = false;
            }
            else
            {
                resultScreen.alpha = 0f;
                resultScreen.blocksRaycasts = false;
            }
        }
    }
}
