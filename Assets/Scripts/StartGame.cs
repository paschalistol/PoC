﻿//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public float score;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("DeathCounter", 0);
        PlayerPrefs.SetFloat("Highscore", 0);
    }


}
