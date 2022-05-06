using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TempData
{
    public int pausedScore;
    public int pausedHealth;
    public float rate;
    public TempData()
    {
       pausedScore = (int)Player.scoreSave;
       pausedHealth = (int)Player.healthSave;
       rate = Spawn.rate;
    }
}
