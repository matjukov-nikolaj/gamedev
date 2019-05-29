using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Wins").GetComponent<Text>().text = PlayerPrefs.GetInt("WINS").ToString();
        GameObject.Find("Loses").GetComponent<Text>().text = PlayerPrefs.GetInt("LOSES").ToString();
    }
}
