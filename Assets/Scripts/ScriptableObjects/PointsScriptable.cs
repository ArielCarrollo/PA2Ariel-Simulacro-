using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointsCounter", menuName = "Points/Points Counter")]
public class PointsScriptable : ScriptableObject
{
    public struct HighScoreEntry
    {
        public float score;
    }

    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();

    public void AddHighScore(float score)
    {
        HighScoreEntry newEntry = new HighScoreEntry { score = score };
        highScores.Add(newEntry);
    }

    public void ResetHighScores()
    {
        highScores.Clear();
    }
}
