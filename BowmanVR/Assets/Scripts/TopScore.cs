using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TopScore
{
    public int topScore;
    public TopScore()
    {
        topScore = (int)Player.score;
    }
}
