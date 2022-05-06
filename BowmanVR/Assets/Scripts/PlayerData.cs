using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int score;
    public int health;
    public int headShots;
    public PlayerData()
    {
        score = (int)Player.score;
        health = (int)Player.health;
        headShots = Player.headShots;
    }
}
