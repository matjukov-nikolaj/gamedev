using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class LevelGenerator
    {
        private Dictionary<int, string> dots= new Dictionary<int, string>();
        
        private List<GameObject> generatedDots = new List<GameObject>();

        public List<GameObject> GetGeneratedList()
        {
            return generatedDots;
        }

        public void generate()
        {
            GenerateDotsDictionary();
            int numberOfDots = Random.Range(3, 24);
            List<string> dotsArray = new List<string>();
            while (numberOfDots != 0)
            {
                int currentNumber = Random.Range(1, 24);
                string value = dots[currentNumber];
                if (!dotsArray.Contains(value))
                {
                    dotsArray.Add(value);
                    --numberOfDots;
                }
            }

            GenerateDotsList(dotsArray);
        }

        private void GenerateDotsList(List<string> dotsArray)
        {
            foreach (var element in dotsArray)
            {
                generatedDots.Add(GameObject.Find(element));
            }
        }

        private void GenerateDotsDictionary()
        {
            dots.Add(1, "Dot11");
            dots.Add(2, "Dot21");
            dots.Add(3, "Dot31");
            dots.Add(4, "Dot41");
            
            dots.Add(5, "Dot12");
            dots.Add(6, "Dot22");
            dots.Add(7, "Dot32");
            dots.Add(8, "Dot42");
            
            dots.Add(9, "Dot13");
            dots.Add(10, "Dot23");
            dots.Add(11, "Dot33");
            dots.Add(12, "Dot43");
            
            dots.Add(13, "Dot14");
            dots.Add(14, "Dot24");
            dots.Add(15, "Dot34");
            dots.Add(16, "Dot44");
            
            dots.Add(17, "Dot15");
            dots.Add(18, "Dot25");
            dots.Add(19, "Dot35");
            dots.Add(20, "Dot45");
            
            dots.Add(21, "Dot16");
            dots.Add(22, "Dot26");
            dots.Add(23, "Dot36");
            dots.Add(24, "Dot46");
        }

    }
}