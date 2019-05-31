//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{


    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private float stepToChangeScore;
    private float scoreShown;
    void Start()
    {

        scoreShown = PlayerPrefs.GetFloat("Highscore", 0);
        scoreText.text = "" + scoreShown.ToString("000000");
    }


    void Update()
    {

        if (scoreShown < PlayerPrefs.GetFloat("Highscore", 0))
        {
            scoreShown += (PlayerPrefs.GetFloat("Highscore", 0) - scoreShown >= stepToChangeScore ? stepToChangeScore : PlayerPrefs.GetFloat("Highscore", 0) - scoreShown);
        }
        else if (scoreShown > PlayerPrefs.GetFloat("Highscore", 0))
        {
            scoreShown -= (scoreShown -PlayerPrefs.GetFloat("Highscore", 0) >= stepToChangeScore ? stepToChangeScore : PlayerPrefs.GetFloat("Highscore", 0) - scoreShown);
        }

        scoreText.text = "Score: " + scoreShown.ToString("000000");

    }

    public static void AddScore(float x)
    {
        PlayerPrefs.SetFloat("Highscore", PlayerPrefs.GetFloat("Highscore", 0) + x) ;
    }
}
