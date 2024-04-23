using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "ScriptableObjects/ScoreData", order = 1)]
public class SO : ScriptableObject
{
    public int score;

    public void IncrementScore(int amount)
    {
        score += amount;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
