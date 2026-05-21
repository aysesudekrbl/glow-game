using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake() { instance = this; }

    public void addScore()
    {
        score ++;
        PlayerPrefs.SetInt("FinalScore", score); 
        PlayerPrefs.Save();
    }

    void Update()
    {
        score = PlayerPrefs.GetInt("FinalScore", 0);
        scoreText.text = score.ToString();
    }
}
