//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("DeathCounter", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
