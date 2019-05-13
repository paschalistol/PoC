using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public float score;
    Text scoreText;
    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
    }

    
    void Update()
    {
        scoreText.text = "Score: " + score;
        
    }

    public void AddScore(float x)
    {
        Debug.Log("2");
        score += x;
        Debug.Log("3");
    }
}
