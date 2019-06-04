//Main Author Johan Ekman
//Secondary Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        
    }
    private void Start()
    {
        Resume();
    }
    public void Resume()
    {
        paused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameController.isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}  
  
