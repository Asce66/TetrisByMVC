using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public int score, topScore, playTimes;

    private void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        score = PlayerPrefs.GetInt("score", 0);
        topScore = PlayerPrefs.GetInt("topScore", 0);
        playTimes = PlayerPrefs.GetInt("playTimes", 0);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("topScore", topScore);
        PlayerPrefs.SetInt("playTimes", playTimes);
        PlayerPrefs.Save();
    }

    public void NextGame()
    {
        playTimes++;
        score = 0;
    }
}
