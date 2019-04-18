using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Slider").GetComponent<Slider>().maxValue = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Slider").GetComponent<Slider>().value = timeLeft;
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
