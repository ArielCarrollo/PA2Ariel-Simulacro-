using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ShowScores : MonoBehaviour
{
    public PointsScriptable pointsScriptable;
    public TextMeshProUGUI highScoreText; 

    void OnEnable()
    {
        UpdateHighScoreText(); 
    }

    void UpdateHighScoreText()
    {
        var sortedScores = pointsScriptable.highScores.OrderByDescending(entry => entry.score).ToList();

        string textToShow = "Top 10 High Scores:\n";
        for (int i = 0; i < Mathf.Min(sortedScores.Count, 10); i++)
        {
            textToShow += (i + 1) + ". " + sortedScores[i].score + "\n";
        }

        highScoreText.text = textToShow;
    }
}