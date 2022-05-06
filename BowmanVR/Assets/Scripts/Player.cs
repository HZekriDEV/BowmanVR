using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Player : MonoBehaviour
{
    public XRController controller;
    public GameManager gameManager;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    public float min = 0f;
    public float maxV = 0.5f;
    public float maxCH = 1f;
    public float rate;
    private bool attacked = false;

    public static float health = 100f;
    public static float score = 0f;
    public static float healthSave = 100f;
    public static float scoreSave = 0f;
    public static int headShots = 0;
    private static float t = 0f;

    List<GameObject> nearEnemy = new List<GameObject>();

    public GameObject postProcessingFX = null;
 
    PostProcessVolume volume;

    Vignette vignette = null;
    ChromaticAberration chromaticAberration = null;
    Bloom bloom = null;
    ColorGrading colorGrade = null;

    void Start()
    {
        if(SaveSystem.fileExists)
        {
            TempData data = SaveSystem.LoadResumeData();

            health = data.pausedHealth;
            score = data.pausedScore;
            Spawn.rate = data.rate;
        }

        volume = postProcessingFX.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
        volume.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        volume.profile.TryGetSettings<Bloom>(out bloom);
        volume.profile.TryGetSettings<ColorGrading>(out colorGrade);
    }


    // Update is called once per frame
    void Update()
    {
        vignette.enabled.value = true;
        chromaticAberration.enabled.value = true;
        bloom.enabled.value = true;
        colorGrade.enabled.value = true;
        
        healthText.text = "Health: " + ((int)health).ToString();
        scoreText.text = "Kills: " + ((int)score).ToString();

        controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool button);
        if (button == true)
        {
            healthSave = health;
            scoreSave = score;
            SaveSystem.PausedPlayer();
            gameManager.LoadLevel("PauseMenu");
        }

        if (nearEnemy.Count > 0)
        {
            attacked = true;
        }
        if(nearEnemy.Count == 0)
        {
            attacked = false;
        }

        if (attacked == true && health > 0)
        {
            health -= 0.05f;

            vignette.intensity.value = Mathf.Lerp(min, maxV, t);
            chromaticAberration.intensity.value = Mathf.Lerp(min, maxCH, t);

            if(vignette.intensity.value < maxV && chromaticAberration.intensity.value < maxCH)
            {
                t += rate * Time.deltaTime;
            }
        }
        if(attacked == false && vignette.intensity.value != 0 && chromaticAberration.intensity.value != 0)
        {
            vignette.intensity.value = Mathf.Lerp(min, maxV, t);
            chromaticAberration.intensity.value = Mathf.Lerp(min, maxCH, t);

            if (vignette.intensity.value > min && chromaticAberration.intensity.value > min)
            {
                t -= rate * Time.deltaTime;
            }

        }
        if(health <= 0)
        {
            print("Dead");
            colorGrade.colorFilter.value.g = 0;
            colorGrade.colorFilter.value.b = 0;
            colorGrade.colorFilter.value.r = 128;
            TopScore data = SaveSystem.LoadTopScore();
            if(SaveSystem.topScoreExists)
            {
                if(score > data.topScore)
                {
                    SaveSystem.SaveTopScore();
                }
            }
            SaveSystem.SavePlayer();
            gameManager.LoadLevel("Menu");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            nearEnemy.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            nearEnemy.Remove(other.gameObject);
        }
    }

    
}
