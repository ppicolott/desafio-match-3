using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;

    public static ScoreManager instance;
    public LevelRules levelRules;

    public TMP_Text scoreText;
    public int tilesClearedCounter;

    private void Awake()
    {
        score = 0;
        instance = this;
        tilesClearedCounter = 0;
    }

    public void AddScore(int _points)
    {
        tilesClearedCounter += _points;
        score = tilesClearedCounter * levelRules.pointsPerTile;
        scoreText.text = "Score: " + score.ToString();
    }
}
