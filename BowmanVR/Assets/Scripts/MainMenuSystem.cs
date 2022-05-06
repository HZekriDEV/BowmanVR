using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuSystem : MonoBehaviour
{
    public float fadeTime = 1f;

    public TextMeshProUGUI Title = null;
    public GameObject Menu = null;

    public TextMeshProUGUI TopScore = null;
    public TextMeshProUGUI LastScore = null;
    public TextMeshProUGUI HeadShots = null;

    private void Awake()
    {
        Player.health = 100;   
        Player.score = 0;
        Spawn.rate = 10f;

        PlayerData data = SaveSystem.LoadPlayer();
        TopScore topScoreData = SaveSystem.LoadTopScore();

        if(SaveSystem.playerExists == false)
        {
            Menu.SetActive(false);
            StartCoroutine(FadeInText(fadeTime, Title));
        }

        LastScore.text = data.score.ToString();
        HeadShots.text = data.headShots.ToString(); 
        TopScore.text = topScoreData.topScore.ToString();


    }
    private IEnumerator FadeInText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
        Menu.SetActive(true);
    }

}
