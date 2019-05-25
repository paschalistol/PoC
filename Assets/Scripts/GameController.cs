using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isPaused;
    
    // Update is called once per frame
    void Update()
    {
        PauseController();
    }

    void PauseController()
    {
        Debug.Log("Paused or not: " + isPaused);
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.T))
            isPaused = !isPaused;
    }
}
