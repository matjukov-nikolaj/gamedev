using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SceneController : MonoBehaviour
    {
        private Dictionary<string, string> results = GameResult.GetParameters();
        
        private List<GameObject> sceneDots = new List<GameObject>();

        private List<GameObject> usedDots;


        void Start()
        {
            StartCoroutine (ShowHelpBox ());
            GenerateSceneDots();
            LevelGenerator.generate();
            StartCoroutine (DrawDotsDinamicly ());
        }
        
        IEnumerator<WaitForSeconds> ShowHelpBox()
        {
            int resultWins = Int32.Parse(results["WINS"]);
            int resultLoses = Int32.Parse(results["LOSES"]);
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
        IEnumerator<WaitForSeconds> DrawDotsDinamicly()
        {
            GenerateSceneDots();
            usedDots = LevelGenerator.GetGeneratedList();
 
            Vector3 prevPos = usedDots[0].transform.position;
            LineRenderer lineRenderer = usedDots[0].GetComponent(typeof(LineRenderer)) as LineRenderer;
            yield return new WaitForSeconds(0.2f);
            usedDots[0].GetComponent<SpriteRenderer>().color = Color.grey;
            for(int i = 1; i < usedDots.Count; ++i)
            {
                yield return new WaitForSeconds(0.5f);
                GameObject dot = usedDots[i];
                Vector3 currPos = dot.transform.position;
                lineRenderer.SetPosition(0, prevPos);
                lineRenderer.numCornerVertices = 5;
                lineRenderer.SetPosition(1, currPos);
                lineRenderer = usedDots[i].GetComponent(typeof(LineRenderer)) as LineRenderer;
                prevPos = currPos;
                usedDots[i].GetComponent<SpriteRenderer>().color = Color.grey;
            }
            Destroy(GameObject.Find("EmptyObject"));
            DontDestroyOnLoad(this.gameObject);
        }

        private void GenerateSceneDots()
        {
            sceneDots.Clear();
            sceneDots.Add(GameObject.Find("Dot11"));
            sceneDots.Add(GameObject.Find("Dot21"));
            sceneDots.Add(GameObject.Find("Dot31"));
            sceneDots.Add(GameObject.Find("Dot41"));
            
            sceneDots.Add(GameObject.Find("Dot12"));
            sceneDots.Add(GameObject.Find("Dot22"));
            sceneDots.Add(GameObject.Find("Dot32"));
            sceneDots.Add(GameObject.Find("Dot42"));
            
            sceneDots.Add(GameObject.Find("Dot13"));
            sceneDots.Add(GameObject.Find("Dot23"));
            sceneDots.Add(GameObject.Find("Dot33"));
            sceneDots.Add(GameObject.Find("Dot43"));
            
            sceneDots.Add(GameObject.Find("Dot14"));
            sceneDots.Add(GameObject.Find("Dot24"));
            sceneDots.Add(GameObject.Find("Dot34"));
            sceneDots.Add(GameObject.Find("Dot44"));
            
            sceneDots.Add(GameObject.Find("Dot15"));
            sceneDots.Add(GameObject.Find("Dot25"));
            sceneDots.Add(GameObject.Find("Dot35"));
            sceneDots.Add(GameObject.Find("Dot45"));
            
            sceneDots.Add(GameObject.Find("Dot16"));
            sceneDots.Add(GameObject.Find("Dot26"));
            sceneDots.Add(GameObject.Find("Dot36"));
            sceneDots.Add(GameObject.Find("Dot46"));
        }

    }
}