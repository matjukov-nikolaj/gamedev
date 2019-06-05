using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    public void GameReset()
    {
        PlayerPrefs.SetInt("WINS", 0);
        PlayerPrefs.SetInt("LOSES", 0);
    }
}
